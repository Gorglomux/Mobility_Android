using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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

            SaleWS vente = (SaleWS)PickingListActivity.data;
            Toast.MakeText(this, "Vente : " + vente.saleNRI,

            ToastLength.Long).Show();
        }
    }
}