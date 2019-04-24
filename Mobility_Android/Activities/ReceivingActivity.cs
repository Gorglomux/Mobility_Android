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
    [Activity(Label = "ReceivingActivity", ParentActivity = typeof(HomeActivity))]
    public class ReceivingActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmReceiving);

            FindViewById<Button>(Resource.Id.btnSelectRecieving).Click += (sender, e) =>
            {
                //Envoi de la donnée à l'autre activité
                String data_to_send = "Hello from ReceivingActivity";
                StartActivity(new Intent(this, typeof(RecievingDetailsActivity)));
            };
        }
    }
}