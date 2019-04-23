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

namespace Mobility_Android.Activities
{

    //Point de départ de l'application
    [Activity(Label = "LoginActivity", MainLauncher = true)]
    public class LoginActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState, Resource.Layout.frmLogin);

            FindViewById<Button>(Resource.Id.btnConnect).Click += (sender, e) =>
            {
                //Todo : Gérer le login
                StartActivity(new Intent(this, typeof(HomeActivity)));
            };
        }
        
    }
}