using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Mobility_Android.Activities
{
    [Activity(Label = "BaseActivity")]
    public abstract class BaseActivity : Activity
    {
        protected void OnCreate(Bundle savedInstanceState, int layoutId)
        {

            base.OnCreate(savedInstanceState);
            base.SetContentView(layoutId);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            if (toolbar != null)
            {
                FindViewById<ImageView>(Resource.Id.imBack).Click += (sender, e) =>
                {
                    
                    Finish();
                };
            }
            SetActionBar(toolbar);
            
        }
    }
}