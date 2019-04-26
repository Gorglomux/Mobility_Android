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
    [Activity(Label = "RecievingDetailsActivity", ParentActivity = typeof(HomeActivity))]
    public class ReceivingDetailsActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmReceivingDetails);

            ReceptionWS reception = (ReceptionWS)ReceivingActivity.data;
            Toast.MakeText(this, "Réception : " + reception.ReceptionNRI,
            ToastLength.Long).Show();

        }
    }
}