using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Mobility_Android.Resources.global;
using Mobility_Android.Resources.webservice;
using Mobility_Android.WebService.Operations;
using Mobility_Android.WebService.Security;
using static Android.InputMethodServices.KeyboardView;
using static CodeParser;

namespace Mobility_Android.Activities
{
    [Activity(Label = "RecievingDetailsActivity", ParentActivity = typeof(HomeActivity))]
    public class ReceivingDetailsActivity : BaseActivity
    {
        public static LicenseWS licence;
        public static List<LicenseWS> licences = null ;
        public static bool mustRefresh = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmReceivingDetails);

            translateScreen();

            // Récupération de la réception
            ReceptionWS reception = (ReceptionWS)ReceivingActivity.data;

            // Action clic pour clear le EditText
            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear), FindViewById<EditText>(Resource.Id.tfLicenseReceivingDetails));

            // Remplir champs de données par rapport à la réception
            FindViewById<TextView>(Resource.Id.tvNumRecieving).Text = reception.ReceptionNRI.ToString();
            FindViewById<TextView>(Resource.Id.tvnameProvider).Text = reception.SupplierCode;

            // Affichage de la dernière licence créer, si pas de licence alors on n'affiche rien
            if (licence!=null)
            {
                if(licence.productNRI != 0)
                {
                    // Creation liste de nom produit
                    List<ProductDetailsWS> listProduct = OperationsWebService.getReceptionProductDetails(Configuration.securityToken, reception.ReceptionNRI, (int)Configuration.currentLanguage, Configuration.userInfos.NRI, null).OfType<ProductDetailsWS>().ToList();

                    // On parcourt la liste de produit pour trouver le produit qui correspond à la licence
                    foreach (ProductDetailsWS p in listProduct)
                    {
                        // Puis on affichage les information dans les TextView
                        if (licence.productNRI == p.NRI)
                        {
                            FindViewById<TextView>(Resource.Id.tvNameProduct).Text = p.code;
                            FindViewById<TextView>(Resource.Id.tvAmountQte).Text = p.qtyPicked.ToString() + "/" + p.qtyToPick.ToString();
                            FindViewById<TextView>(Resource.Id.tvAmountPoids).Text = licence.weightKG.ToString() + " kg";
                        }
                    }
                }
                

            }

            // Action touche "Enter" pour accèder à la création d'une nouvelle licence
            EditText urlEditText = FindViewById<EditText>(Resource.Id.tfLicenseReceivingDetails);
            urlEditText.KeyPress += (object sender, View.KeyEventArgs e) => {
                e.Handled = false;
                CodeParser parser = new CodeParser();

                if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
                {
                    if (urlEditText.Text.ToString() != "")
                    {
                        urlEditText.Text.Replace(" ", "");
                        licence = new LicenseWS();

                        ParsedLicence parsedLicence = parser.getLicense(urlEditText.Text);
                        licence = Converts.ParsedLicToLicenceWS(parsedLicence);

                        if (licence.licenseCode == null)
                        {
                            licence.licenseCode = urlEditText.Text;
                            licence.parentNRI = reception.ReceptionNRI;
                            data = reception;
                            StartActivity(new Intent(this, typeof(NewLicenseActivity)));
                        } else
                        {
                            licence.parentNRI = reception.ReceptionNRI;
                            // Creation liste de nom produit
                            List<ProductDetailsWS> listProduct = OperationsWebService.getReceptionProductDetails(Configuration.securityToken, reception.ReceptionNRI, (int)Configuration.currentLanguage, Configuration.userInfos.Udp_NRI, Configuration.userInfos.Udp_Label).OfType<ProductDetailsWS>().ToList();


                            foreach (ProductDetailsWS pro in listProduct)
                            {
                                if (pro.SSCC.Contains(licence.productSSCC))
                                {
                                    licence.productNRI = pro.NRI;
                                }
                            }

                            if(licence.productNRI != 0)
                            {
                                data = reception;
                                var produit = OperationsWebService.pickLicenseReception(Configuration.securityToken, licence, (int)Configuration.currentLanguage, Configuration.userInfos.Udp_NRI, Configuration.userInfos.Udp_Label);
                                if (produit == null)
                                {
                                    Toast.MakeText(this, OperationsWebService.errorMessage, ToastLength.Long).Show();
                                    Recreate();
                                }
                                else
                                {
                                    Recreate();
                                }
                            } else
                            {
                                if(Configuration.currentLanguage == CR_TTLangue.French_Canada)
                                {
                                    Toast.MakeText(this, Activities.ResourceFR.errProductNotPresent, ToastLength.Long).Show() ;
                                } else
                                {
                                    Toast.MakeText(this, Activities.ResourceEN.errProductNotPresent, ToastLength.Long).Show();
                                }
                            }
                            
                        }

                        e.Handled = true;
                    }
                    else
                    {
                        urlEditText.Text = "";
                        switch (Configuration.currentLanguage){
                            case  CR_TTLangue.French_Canada:
                                Toast.MakeText(this, Activities.ResourceFR.errEmptyFieldLicense, ToastLength.Long).Show();
                                break;
                            
                            case  CR_TTLangue.English:
                                Toast.MakeText(this, Activities.ResourceFR.errEmptyFieldLicense, ToastLength.Long).Show();
                                break;
                            
                        }
                        
                    }
                }
            };

            // Action clic sur détails pour accèder à la liste de produit d'une reception
            FindViewById<ImageButton>(Resource.Id.imDetails).Click += async (sender, e) => {
                data = reception;
                IsBusy = true;
                await Task.Delay(50);
                StartActivity(new Intent(this, typeof(ProductDetailsActivity)));
                IsBusy = false;
            };

            // Ne peut pas fonctionner, il faudrait ajouter une méthode dans les web services afin d'ajouter des licences sur une palette.

            /*// Action clic sur palette pour mettre toutes les licences crées sur la palette
            FindViewById<Button>(Resource.Id.btnPalette).Click += async (sender, e) => {
                if (licences != null)
                {

                    IsBusy = true;
                    await Task.Delay(50);
                    var test1 = "1302";
                    var test2 = "111";
                    var codePalette = OperationsWebService.createPalletCode(Configuration.securityToken, ref test1, ref test2);
                    string msg = "Licence non transférée : ";
                    bool isSucess = true;
                    licences.Remove(licence);

                    foreach (LicenseWS lic in licences)
                    {
                        if (!(OperationsWebService.relocateLicense(Configuration.securityToken, lic.licenseCode, codePalette, RELOCATION_DESTINATION.Pallet, Configuration.userInfos.warehouseNRI)))
                        {
                            isSucess = false;
                            msg += lic.licenseCode + ", ";
                        }
                    }
                    IsBusy = false;
                    if (isSucess)
                    {
                        Toast.MakeText(this, "Les licences ont été transférées", ToastLength.Long).Show();

                    }
                    else
                    {
                        Toast.MakeText(this, msg, ToastLength.Long).Show();

                    }
                    Recreate();
                }
                else
                {
                    Toast.MakeText(this, "Veuillez insérer des licences", ToastLength.Long).Show();
                }
            };*/

            // Action clic sur bouton pour completer une reception
            FindViewById<Button>(Resource.Id.btnEndReceiving).Click += async(sender, e) => {
                IsBusy = true;
                await Task.Delay(50);
                OperationsWebService.completeReception(Configuration.securityToken, reception.ReceptionNRI);
                IsBusy = false;
                mustRefresh = true;
                
                Finish();
            };
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            if (hasFocus && NewLicenseActivity.mustRefresh)
            {
                NewLicenseActivity.mustRefresh = false;
                Recreate();
            }
            
        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                {
                    FindViewById<TextView>(Resource.Id.tvTitleRecieving).Text = Activities.ResourceFR.tvTitleRecieving;
                    FindViewById<TextView>(Resource.Id.tvRecieving).Text = Activities.ResourceFR.tvRecieving;
                    FindViewById<TextView>(Resource.Id.tvProvider).Text = Activities.ResourceFR.tvProvider;
                    FindViewById<TextView>(Resource.Id.tvProduct).Text = Activities.ResourceFR.tvProduct;
                    FindViewById<TextView>(Resource.Id.tvQte).Text = Activities.ResourceFR.tvQte;
                    FindViewById<TextView>(Resource.Id.tvPoids).Text = Activities.ResourceFR.tvPoids;
                    FindViewById<TextView>(Resource.Id.tvLicence).Text = Activities.ResourceFR.tvLicence;
                    FindViewById<Button>(Resource.Id.btnEndReceiving).Text = Activities.ResourceFR.btnEndReceiving;

                    break;
                }

                case CR_TTLangue.English:
                {
                    FindViewById<TextView>(Resource.Id.tvTitleRecieving).Text = Activities.ResourceEN.tvTitleRecieving;
                    FindViewById<TextView>(Resource.Id.tvRecieving).Text = Activities.ResourceEN.tvRecieving;
                    FindViewById<TextView>(Resource.Id.tvProvider).Text = Activities.ResourceEN.tvProvider;
                    FindViewById<TextView>(Resource.Id.tvProduct).Text = Activities.ResourceEN.tvProduct;
                    FindViewById<TextView>(Resource.Id.tvQte).Text = Activities.ResourceEN.tvQte;
                    FindViewById<TextView>(Resource.Id.tvPoids).Text = Activities.ResourceEN.tvPoids;
                    FindViewById<TextView>(Resource.Id.tvLicence).Text = Activities.ResourceEN.tvLicence;
                    FindViewById<Button>(Resource.Id.btnEndReceiving).Text = Activities.ResourceEN.btnEndReceiving;
                    break;
                }
            }
        }
    }
}