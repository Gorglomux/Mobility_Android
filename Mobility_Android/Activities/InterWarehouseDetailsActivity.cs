﻿using System;
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
    /**
     * Fonctionnalité supprimée pour l'instant
     * 
     **/
    [Activity(Label = "InterWarehouseDetailsActivity", ParentActivity = typeof(HomeActivity))]
    public class InterWarehouseDetailsActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmInterWarehouseDetails);
            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear), FindViewById<EditText>(Resource.Id.tfLicenseInterWarehouseDetails));
            // Create your application here
        }
    }
}