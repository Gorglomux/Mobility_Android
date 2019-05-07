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
    [Activity(Label = "ProductionActivity", ParentActivity = typeof(HomeActivity))]
    public class ProductionActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmProduction);

            translateScreen();

            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear), FindViewById<EditText>(Resource.Id.tfLicenceProduction));
            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear2), FindViewById<EditText>(Resource.Id.tfStation));          
        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<TextView>(Resource.Id.tvPutProduction).Text = Activities.ResourceFR.tvPutProduction;
                        FindViewById<TextView>(Resource.Id.tvLicence).Text = Activities.ResourceFR.tvLicence;
                        FindViewById<TextView>(Resource.Id.tvStation).Text = Activities.ResourceFR.tvStation;
                        FindViewById<TextView>(Resource.Id.tvProductEntete).Text = Activities.ResourceFR.tvProductEntete;
                        FindViewById<TextView>(Resource.Id.tvQtyEntete).Text = Activities.ResourceFR.tvQtyEntete;
                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<TextView>(Resource.Id.tvPutProduction).Text = Activities.ResourceEN.tvPutProduction;
                        FindViewById<TextView>(Resource.Id.tvLicence).Text = Activities.ResourceEN.tvLicence;
                        FindViewById<TextView>(Resource.Id.tvStation).Text = Activities.ResourceEN.tvStation;
                        FindViewById<TextView>(Resource.Id.tvProductEntete).Text = Activities.ResourceEN.tvProductEntete;
                        FindViewById<TextView>(Resource.Id.tvQtyEntete).Text = Activities.ResourceEN.tvQtyEntete;
                        FindViewById<TextView>(Resource.Id.tvQtyEntete).Text = Activities.ResourceEN.tvQtyEntete;
                        break;
                    }
            }
        }
    }
}