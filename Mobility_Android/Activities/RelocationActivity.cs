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
    [Activity(Label = "RelocationActivity", ParentActivity = typeof(HomeActivity))]
    public class RelocationActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmRelocation);
            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear), FindViewById<EditText>(Resource.Id.tfLicenseRelocation));
            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear2), FindViewById<EditText>(Resource.Id.tfDestination));

            // Sélection Licence


            //Gestion spinner locations


        }
    }
}