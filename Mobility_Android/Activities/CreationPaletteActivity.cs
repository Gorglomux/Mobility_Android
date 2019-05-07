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
     *  A CODER PLUS TARD
     * 
     * 
     * */
    [Activity(Label = "CreationPaletteActivity", ParentActivity = typeof(HomeActivity))]
    public class CreationPaletteActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmCreationPalette);

            translateScreen();
        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<TextView>(Resource.Id.tvCreation).Text = Activities.ResourceFR.tvCreation;
                        FindViewById<TextView>(Resource.Id.tvLigne).Text = Activities.ResourceFR.tvLigne;
                        FindViewById<TextView>(Resource.Id.tvGabarit).Text = Activities.ResourceFR.tvGabarit;
                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<TextView>(Resource.Id.tvCreation).Text = Activities.ResourceEN.tvCreation;
                        FindViewById<TextView>(Resource.Id.tvLigne).Text = Activities.ResourceEN.tvLigne;
                        FindViewById<TextView>(Resource.Id.tvGabarit).Text = Activities.ResourceEN.tvGabarit;
                        break;
                    }
            }
        }
    }
}