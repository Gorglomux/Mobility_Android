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
     * Non supporté pour le moment
     * 
     * 
     * */
    [Activity(Label = "CreationPaletteActivity", ParentActivity = typeof(HomeActivity))]
    public class CreationPaletteActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmCreationPalette);

            // Create your application here
        }
    }
}