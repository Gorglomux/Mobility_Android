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
using Mobility_Android.Resources.global;
using Mobility_Android.WebService.Security;

namespace Mobility_Android.Activities
{
    /**
     * A CODER PLUS TARD
     * 
     **/ 
    [Activity(Label = "CsDetailsProductionActivity", ParentActivity = typeof(HomeActivity))]
    public class CsDetailsProductionActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmCsDetailsProduction);

            translateScreen();
        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<TextView>(Resource.Id.tvProductDetails).Text = Activities.ResourceFR.tvProductDetails;
                        FindViewById<TextView>(Resource.Id.tvCS).Text = Activities.ResourceFR.tvCS;
                        FindViewById<TextView>(Resource.Id.tvWeight).Text = Activities.ResourceFR.tvWeight;
                        FindViewById<TextView>(Resource.Id.tvSelect).Text = Activities.ResourceFR.tvSelect;
                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<TextView>(Resource.Id.tvProductDetails).Text = Activities.ResourceEN.tvProductDetails;
                        FindViewById<TextView>(Resource.Id.tvCS).Text = Activities.ResourceEN.tvCS;
                        FindViewById<TextView>(Resource.Id.tvWeight).Text = Activities.ResourceEN.tvWeight;
                        FindViewById<TextView>(Resource.Id.tvSelect).Text = Activities.ResourceEN.tvSelect;
                        break;
                    }
            }
        }
    }
}