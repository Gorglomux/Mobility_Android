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
    [Activity(Label = "ProductionOrderActivity", ParentActivity = typeof(HomeActivity))]
    public class ProductionOrderActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmProductionOrder);

            translateScreen();

            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear), FindViewById<EditText>(Resource.Id.tfLicenseProductOrder));
        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<TextView>(Resource.Id.tvTitleProdOrder).Text = Activities.ResourceFR.tvTitleProdOrder;
                        FindViewById<TextView>(Resource.Id.tvProdOrder).Text = Activities.ResourceFR.tvProdOrder;
                        FindViewById<TextView>(Resource.Id.tvProduct).Text = Activities.ResourceFR.tvProduct;
                        FindViewById<TextView>(Resource.Id.tvQte).Text = Activities.ResourceFR.tvQte;
                        FindViewById<TextView>(Resource.Id.tvPoids).Text = Activities.ResourceFR.tvPoids;
                        FindViewById<TextView>(Resource.Id.tvLicense).Text = Activities.ResourceFR.tvLicense;
                        FindViewById<Button>(Resource.Id.btnEndPickingPalette).Text = Activities.ResourceFR.btnEndPickingPalette;

                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<TextView>(Resource.Id.tvTitleProdOrder).Text = Activities.ResourceEN.tvTitleProdOrder;
                        FindViewById<TextView>(Resource.Id.tvProdOrder).Text = Activities.ResourceEN.tvProdOrder;
                        FindViewById<TextView>(Resource.Id.tvProduct).Text = Activities.ResourceEN.tvProduct;
                        FindViewById<TextView>(Resource.Id.tvQte).Text = Activities.ResourceEN.tvQte;
                        FindViewById<TextView>(Resource.Id.tvPoids).Text = Activities.ResourceEN.tvPoids;
                        FindViewById<TextView>(Resource.Id.tvLicense).Text = Activities.ResourceEN.tvLicense;
                        FindViewById<Button>(Resource.Id.btnEndPickingPalette).Text = Activities.ResourceEN.btnEndPickingPalette;
                        break;
                    }
            }
        }
    }
}