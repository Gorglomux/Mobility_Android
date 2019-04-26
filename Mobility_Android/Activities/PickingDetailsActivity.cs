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
    [Activity(Label = "PickingDetailsActivity", ParentActivity = typeof(HomeActivity))]
    public class PickingDetailsActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmPickingDetails);

            SaleWS reception = (SaleWS)PickingListActivity.data;
            Toast.MakeText(this, "Vente : " + reception.saleNRI,
            ToastLength.Long).Show();
        }
    }
}