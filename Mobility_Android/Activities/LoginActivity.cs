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
using Mobility_Android.Resources.webservice;

namespace Mobility_Android.Activities
{

    //Point de départ de l'application
    [Activity(Label = "LoginActivity", MainLauncher = true)]
    public class LoginActivity : BaseActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmLogin);
            EditText username = FindViewById<EditText>(Resource.Id.tfName);
            EditText password = FindViewById<EditText>(Resource.Id.tfPass);

            FindViewById<Button>(Resource.Id.btnConnect).Click += (sender, e) =>
            {
                if(UserWebService.doLogin(username.Text, password.Text))
                {
                    StartActivity(new Intent(this, typeof(HomeActivity)));
                } else
                {
                    Toast.MakeText(this, "Erreur connexion", ToastLength.Long).Show();
                }
                
            };
                        FindViewById<ImageButton>(Resource.Id.imLogout).Click += (sender, e) =>
            {
               StartActivity(new Intent(this, typeof(LogoutActivity)));
            };

            FindViewById<ImageButton>(Resource.Id.imConfig).Click += (sender, e) =>
            {
                StartActivity(new Intent(this, typeof(ConfigActivity)));
            };
        }
        
    }
}