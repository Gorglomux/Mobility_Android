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

/**
 * A implémenter plus tard
 * */
namespace Mobility_Android.Activities
{
    [Activity(Label = "WarehouseListActivity", ParentActivity = typeof(HomeActivity))]
    public class WarehouseListActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmWarehouseList);
        }
    }
}