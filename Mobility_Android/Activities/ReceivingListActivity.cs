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
    [Activity(Label = "ReceivingListActivity", ParentActivity = typeof(HomeActivity))]
    public class ReceivingListActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmReceivingList);

            translateScreen();
        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<TextView>(Resource.Id.tvPicking).Text = Activities.ResourceFR.tvPicking;
                        FindViewById<TextView>(Resource.Id.tvLine).Text = Activities.ResourceFR.tvLine;
                        FindViewById<TextView>(Resource.Id.tvGabarit).Text = Activities.ResourceFR.tvGabarit;

                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<TextView>(Resource.Id.tvPicking).Text = Activities.ResourceEN.tvPicking;
                        FindViewById<TextView>(Resource.Id.tvLine).Text = Activities.ResourceEN.tvLine;
                        FindViewById<TextView>(Resource.Id.tvProvider).Text = Activities.ResourceEN.tvProvider;
                        break;
                    }
            }
        }
    }
}