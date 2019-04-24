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

namespace Mobility_Android.Activities
{
    [Activity(Label = "PickingListActivity", ParentActivity = typeof(HomeActivity))]
    public class PickingListActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmPickingList);

            FindViewById<Button>(Resource.Id.btnNewSale).Click += (sender, e) =>
            {
                StartActivity(new Intent(this, typeof(NewSaleActivity)));
            };
        }
    }
}