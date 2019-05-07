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
    [Activity(Label = "ProductionMenuActivity", ParentActivity = typeof(HomeActivity))]
    public class ProductionMenuActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmProductionMenu);

            translateScreen();

            FindViewById<Button>(Resource.Id.btnPutProduction).Click += (sender, e) =>
            {
                StartActivity(new Intent(this, typeof(ProductionActivity)));
            };
            FindViewById<Button>(Resource.Id.btnAddPalette).Click += (sender, e) =>
            {
                StartActivity(new Intent(this, typeof(ProductionOrderActivity)));
            };
        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<Button>(Resource.Id.btnPutProduction).Text = Activities.ResourceFR.btnPutProduction;
                        FindViewById<Button>(Resource.Id.btnAddPalette).Text = Activities.ResourceFR.btnAddPalette;

                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<Button>(Resource.Id.btnPutProduction).Text = Activities.ResourceEN.btnPutProduction;
                        FindViewById<Button>(Resource.Id.btnAddPalette).Text = Activities.ResourceEN.btnAddPalette;
                        break;
                    }
            }
        }
    }
}