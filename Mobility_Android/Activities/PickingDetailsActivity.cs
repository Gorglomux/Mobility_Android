using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mobility_Android.Resources.global;
using Mobility_Android.Resources.webservice;
using Mobility_Android.WebService.Operations;
using Mobility_Android.WebService.Security;
using static CodeParser;

namespace Mobility_Android.Activities
{

    /**
     * 
     * Interface pour scanner des produits pour les rajouter dans une commande
     * 
     * */
    [Activity(Label = "PickingDetailsActivity", ParentActivity = typeof(HomeActivity))]
    public class PickingDetailsActivity : BaseActivity
    {
        public static LicenseWS licence;
        public static bool mustRefresh = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmPickingDetails);

            translateScreen();

            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear), FindViewById<EditText>(Resource.Id.tfLicensePickingDetails));

            SaleWS sale = (SaleWS)PickingListActivity.data;


            // Remplir champs de données par rapport à la réception
            FindViewById<TextView>(Resource.Id.tvNumPicking).Text = sale.saleNRI.ToString();
            FindViewById<TextView>(Resource.Id.tvnameClient).Text = sale.customerCode;

            // Action clic sur détails pour accèder à la liste de produit d'une reception
            FindViewById<ImageButton>(Resource.Id.imDetails).Click += async (sender, e) => {
                data = sale;
                IsBusy = true;
                await Task.Delay(50);
                StartActivity(new Intent(this, typeof(ProductDetailsActivity)));
                IsBusy = false;
            };

            // Action clic sur bouton pour completer une reception
            FindViewById<Button>(Resource.Id.btnEndPicking).Click += async (sender, e) => {
                IsBusy = true;
                await Task.Delay(50);
                OperationsWebService.completeSale(Configuration.securityToken, sale.saleNRI);
                IsBusy = false;
                mustRefresh = true;
                Finish();
            };

            // Affichage de la dernière licence créer, si pas de licence alors on n'affiche rien
            if (licence != null)
            {
                // Creation liste de nom produit
                List<ProductDetailsWS> listProduct = OperationsWebService.getSaleProductDetails(Configuration.securityToken, sale.saleNRI, (int)Configuration.currentLanguage, Configuration.userInfos.Udp_NRI).OfType<ProductDetailsWS>().ToList();

                foreach (ProductDetailsWS p in listProduct)
                    // On parcourt la liste de produit pour trouver le produit qui correspond à la licence
                    foreach (PickedLicensesWS l in p.pickedProducts)
                    {
                        // Puis on affichage les information dans les TextView
                        if (licence.licenseCode == l.code)
                        {
                            FindViewById<TextView>(Resource.Id.tvNameProduct).Text = p.code;
                            FindViewById<TextView>(Resource.Id.tvAmountQte).Text = p.qtyPicked.ToString();
                            FindViewById<TextView>(Resource.Id.tvAmountPoids).Text = l.weight.ToString() + " kg";
                        }
                    }
            }

            EditText editText = FindViewById<EditText>(Resource.Id.tfLicensePickingDetails);
            editText.KeyPress += (object sender, View.KeyEventArgs e) => {
                e.Handled = false;

                CodeParser parser = new CodeParser();


                if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
                {
                    if (editText.Text.ToString() != "")
                    {
                        editText.Text.Replace(" ", "");
                        licence = new LicenseWS();

                        ParsedLicence parsedLicence = parser.getLicense(editText.Text);
                        licence = Converts.ParsedLicToLicenceWS(parsedLicence);
                        
                        if(licence.licenseCode == null)
                        {
                            licence.licenseCode = editText.Text;
                        }
                        
                        licence.parentNRI = sale.saleNRI;

                        var productResult = OperationsWebService.PickLicenseSale(Configuration.securityToken, licence, Configuration.userInfos.warehouseNRI, Configuration.userInfos.warehouseNRI, Configuration.userInfos.Udp_NRI);
                        if (productResult == null)
                        {
                            Toast.MakeText(this, OperationsWebService.errorMessage, ToastLength.Long).Show();
                            OperationsWebService.errorMessage = "";
                        } else
                        {
                            switch (Configuration.currentLanguage)
                            {
                                case CR_TTLangue.French_Canada:
                                    {
                                        Toast.MakeText(this, "Licence ajoutée", ToastLength.Long).Show();

                                        break;
                                    }

                                case CR_TTLangue.English:
                                    {
                                        Toast.MakeText(this, "License added", ToastLength.Long).Show();
                                        break;
                                    }
                            }

                           
                        }
                        data = sale;
                        
                        Recreate();
                        e.Handled = true;
                    }
                    else
                    {
                        editText.Text = "";
                        switch (Configuration.currentLanguage)
                        {
                            case CR_TTLangue.French_Canada:
                                {
                                    Toast.MakeText(this, Activities.ResourceFR.errEmptyFieldLicense, ToastLength.Long).Show();

                                    break;
                                }

                            case CR_TTLangue.English:
                                {
                                    Toast.MakeText(this, Activities.ResourceEN.errEmptyFieldLicense, ToastLength.Long).Show();
                                    break;
                                }
                        }

                         
                    }
                }
            };
        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<TextView>(Resource.Id.tvTitlePicking).Text = Activities.ResourceFR.tvTitlePicking;
                        FindViewById<TextView>(Resource.Id.tvPicking).Text = Activities.ResourceFR.tvPicking;
                        FindViewById<TextView>(Resource.Id.tvClient).Text = Activities.ResourceFR.tvClient;
                        FindViewById<TextView>(Resource.Id.tvProduct).Text = Activities.ResourceFR.tvProduct;
                        FindViewById<TextView>(Resource.Id.tvQte).Text = Activities.ResourceFR.tvQte;
                        FindViewById<TextView>(Resource.Id.tvPoids).Text = Activities.ResourceFR.tvPoids;
                        FindViewById<TextView>(Resource.Id.tvLicense).Text = Activities.ResourceFR.tvLicense;
                        FindViewById<Button>(Resource.Id.btnEndPicking).Text = Activities.ResourceFR.btnEndPicking;
                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<TextView>(Resource.Id.tvTitlePicking).Text = Activities.ResourceEN.tvTitlePicking;
                        FindViewById<TextView>(Resource.Id.tvPicking).Text = Activities.ResourceEN.tvPicking;
                        FindViewById<TextView>(Resource.Id.tvClient).Text = Activities.ResourceEN.tvClient;
                        FindViewById<TextView>(Resource.Id.tvProduct).Text = Activities.ResourceEN.tvProduct;
                        FindViewById<TextView>(Resource.Id.tvQte).Text = Activities.ResourceEN.tvQte;
                        FindViewById<TextView>(Resource.Id.tvPoids).Text = Activities.ResourceEN.tvPoids;
                        FindViewById<TextView>(Resource.Id.tvLicense).Text = Activities.ResourceEN.tvLicense;
                        FindViewById<Button>(Resource.Id.btnEndPicking).Text = Activities.ResourceEN.btnEndPicking;
                        break;
                    }
            }
        }
    }
}