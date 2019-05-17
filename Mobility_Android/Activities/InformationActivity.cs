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
    [Activity(Label = "InformationActivity", ParentActivity = typeof(HomeActivity))]
    public class InformationActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmInfo);

            translateScreen();
        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<TextView>(Resource.Id.tvInfo).Text = Activities.ResourceFR.tvInfo;
                        FindViewById<TextView>(Resource.Id.tvAddress1).Text = Activities.ResourceFR.tvAddress1;
                        FindViewById<TextView>(Resource.Id.tvAddress2).Text = Activities.ResourceFR.tvAddress2;
                        FindViewById<TextView>(Resource.Id.tvPhoneNumber).Text = Activities.ResourceFR.tvPhoneNumber;
                        FindViewById<TextView>(Resource.Id.tvEmail).Text = Activities.ResourceFR.tvEmail;
                        FindViewById<TextView>(Resource.Id.tvWebsite).Text = Activities.ResourceFR.tvWebsite;
                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<TextView>(Resource.Id.tvInfo).Text = Activities.ResourceEN.tvInfo;
                        FindViewById<TextView>(Resource.Id.tvAddress1).Text = Activities.ResourceEN.tvAddress1;
                        FindViewById<TextView>(Resource.Id.tvAddress2).Text = Activities.ResourceEN.tvAddress2;
                        FindViewById<TextView>(Resource.Id.tvPhoneNumber).Text = Activities.ResourceEN.tvPhoneNumber;
                        FindViewById<TextView>(Resource.Id.tvEmail).Text = Activities.ResourceEN.tvEmail;
                        FindViewById<TextView>(Resource.Id.tvWebsite).Text = Activities.ResourceEN.tvWebsite;
                        break;
                    }
            }
        }

    }
}