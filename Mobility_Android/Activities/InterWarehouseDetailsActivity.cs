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
    [Activity(Label = "InterWarehouseDetailsActivity", ParentActivity = typeof(HomeActivity))]
    public class InterWarehouseDetailsActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmInterWarehouseDetails);
            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear), FindViewById<EditText>(Resource.Id.tfLicenseInterWarehouseDetails));

            translateScreen();
        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<TextView>(Resource.Id.tvTitleInterWarehouse).Text = Activities.ResourceFR.tvTitleInterWarehouse;
                        FindViewById<TextView>(Resource.Id.tvInterWarehouse).Text = Activities.ResourceFR.tvInterWarehouse;
                        FindViewById<TextView>(Resource.Id.tvDestination).Text = Activities.ResourceFR.tvDestination;
                        FindViewById<TextView>(Resource.Id.tvProduct).Text = Activities.ResourceFR.tvProduct;
                        FindViewById<TextView>(Resource.Id.tvQte).Text = Activities.ResourceFR.tvTitleInterWarehouse;
                        FindViewById<TextView>(Resource.Id.tvPoids).Text = Activities.ResourceFR.tvTitleInterWarehouse;
                        FindViewById<TextView>(Resource.Id.tvLicense).Text = Activities.ResourceFR.tvTitleInterWarehouse;
                        FindViewById<Button>(Resource.Id.btnEnd).Text = Activities.ResourceFR.btnMove;
                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<TextView>(Resource.Id.tvTitleInterWarehouse).Text = Activities.ResourceEN.tvTitleInterWarehouse;
                        FindViewById<TextView>(Resource.Id.tvInterWarehouse).Text = Activities.ResourceEN.tvInterWarehouse;
                        FindViewById<TextView>(Resource.Id.tvDestination).Text = Activities.ResourceEN.tvDestination;
                        FindViewById<TextView>(Resource.Id.tvProduct).Text = Activities.ResourceEN.tvProduct;
                        FindViewById<TextView>(Resource.Id.tvQte).Text = Activities.ResourceEN.tvProduct;
                        FindViewById<TextView>(Resource.Id.tvPoids).Text = Activities.ResourceEN.tvProduct;
                        FindViewById<TextView>(Resource.Id.tvLicense).Text = Activities.ResourceEN.tvProduct;
                        FindViewById<Button>(Resource.Id.btnEnd).Text = Activities.ResourceEN.btnMove;
                        break;
                    }
            }
        }
    }
}