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
using Java.Lang;
using Mobility_Android.Resources.global;
using Mobility_Android.WebService.Security;

namespace Mobility_Android.Activities
{
    [Activity(Label = "LogoutActivity", ParentActivity = typeof(HomeActivity))]
    public class LogoutActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmLogout);

            translateScreen();

            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear), FindViewById<EditText>(Resource.Id.tfPassPrompt));
            FindViewById<Button>(Resource.Id.btnLogout).Click += (sender, e) =>
             {
                 var password = FindViewById<EditText>(Resource.Id.tfPassPrompt).Text;
                 if (password == Configuration.logoutPassword)
                 {
                     JavaSystem.Exit(0);
                 }
                 else
                 {
                     switch (Configuration.currentLanguage)
                     {
                         case CR_TTLangue.French_Canada:
                             {
                                 Toast.MakeText(this, "Mot de passe invalide", ToastLength.Short).Show();
                                 break;
                             }

                         case CR_TTLangue.English:
                             {
                                 Toast.MakeText(this, "Invalid password", ToastLength.Short).Show();
                                 break;
                             }
                     }
                 }
             };
        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<TextView>(Resource.Id.tvLogout).Text = Activities.ResourceFR.tvLogout;
                        FindViewById<TextView>(Resource.Id.tvPassPrompt).Text = Activities.ResourceFR.tvPassPrompt;
                        FindViewById<Button>(Resource.Id.btnLogout).Text = Activities.ResourceFR.btnLogout;
                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<TextView>(Resource.Id.tvLogout).Text = Activities.ResourceEN.tvLogout;
                        FindViewById<TextView>(Resource.Id.tvPassPrompt).Text = Activities.ResourceEN.tvPassPrompt;
                        FindViewById<Button>(Resource.Id.btnLogout).Text = Activities.ResourceEN.btnLogout;
                        break;
                    }
            }
        }
    }
}