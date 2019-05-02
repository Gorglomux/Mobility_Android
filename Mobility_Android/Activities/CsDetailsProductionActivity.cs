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
    /**
     * A implémenter
     * 
     **/ 
    [Activity(Label = "CsDetailsProductionActivity", ParentActivity = typeof(HomeActivity))]
    public class CsDetailsProductionActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmCsDetailsProduction);

            // Create your application here
        }
    }
}