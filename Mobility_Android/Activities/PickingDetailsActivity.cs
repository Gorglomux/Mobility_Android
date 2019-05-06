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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmPickingDetails);
            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear), FindViewById<EditText>(Resource.Id.tfLicensePickingDetails));

            SaleWS sale = (SaleWS)PickingListActivity.data;

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

                Finish();
            };

            EditText editText = FindViewById<EditText>(Resource.Id.tfLicensePickingDetails);
            editText.KeyPress += (object sender, View.KeyEventArgs e) => {
                e.Handled = false;
                if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
                {
                    if (editText.Text.ToString() != "")
                    {
                        LicenseWS licence = new LicenseWS();
                        licence.licenseCode = editText.Text;
                        licence.parentNRI = sale.saleNRI;
                        if(OperationsWebService.PickLicenseSale(Configuration.securityToken, licence, Configuration.userInfos.warehouseNRI, Configuration.userInfos.warehouseNRI) == null)
                        {
                            Toast.MakeText(this, "Vérifier le numéro de licence", ToastLength.Long).Show();
                        } else
                        {
                            Toast.MakeText(this, "Licence ajoutée", ToastLength.Long).Show();
                        }
                        data = sale;
                        
                        refresh();
                        e.Handled = true;
                    }
                    else
                    {
                        editText.Text = "";
                        Toast.MakeText(this, "Veuillez entrer un code pour la licence", ToastLength.Long).Show();
                    }
                }
            };
        }
    }
}