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

namespace Mobility_Android.Activities
{
    [Activity(Label = "LogoutActivity", ParentActivity = typeof(HomeActivity))]
    public class LogoutActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmLogout);
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
                     Toast.MakeText(this, "Mot de passe invalide", ToastLength.Short).Show();
                 }
             };
        }
    }
}