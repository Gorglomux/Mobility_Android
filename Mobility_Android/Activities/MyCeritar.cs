using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
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

    /*
     * Point de départ de l'application
     * Permet de se connecter à l'application, de se déconnecter ou d'accéder aux paramètres et de modifier la configuration
     * 
     **/ 
    [Activity(Label = "MyCeritar", MainLauncher = true)]
    public class MyCeritar : BaseActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmLogin);
            
            //On récupère le contenu des champs       
            EditText username = FindViewById<EditText>(Resource.Id.tfName);
            EditText password = FindViewById<EditText>(Resource.Id.tfPass);

            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear), username);
            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear2), password);

            //Si l'utilisateur appuie sur le bouton connecter
            FindViewById<Button>(Resource.Id.btnConnect).Click += async  (sender, e) =>
            {


                //L'application est occupée
                IsBusy = true;
                await Task.Delay(50);
                //On essaye de se connecter au WebService
                if (UserWebService.doLogin(username.Text, password.Text))
                {
                    //Si la connectio s'est bien passé on est envoyé vers la page d'accueil

                    StartActivity(new Intent(this, typeof(HomeActivity)));
                } else
                {
                    Toast.MakeText(this, "Erreur connexion", ToastLength.Short).Show();
                }
                //La ressource n'est plus occupée
                IsBusy = false;

            };
            //Si l'utilisateur appuie sur Logout
            FindViewById<ImageButton>(Resource.Id.imLogout).Click += (sender, e) =>
            {
               StartActivity(new Intent(this, typeof(LogoutActivity)));
            };
            //Si l'utilisateur appuie sur Config
            FindViewById<ImageButton>(Resource.Id.imConfig).Click += (sender, e) =>
            {
                StartActivity(new Intent(this, typeof(ConfigActivity)));
            };


            var rbFrench = FindViewById<RadioButton>(Resource.Id.rbFrench);
            rbFrench.Click += (sender, e) =>
            {
                Toast.MakeText(this, rbFrench.Text, ToastLength.Short).Show();
                SetLocale(new CultureInfo("fr"));

            };

            var rbEnglish= FindViewById<RadioButton>(Resource.Id.rbEnglish);
            rbEnglish.Click += (sender, e) =>
            {
                Toast.MakeText(this, rbEnglish.Text, ToastLength.Short).Show();
                SetLocale(new CultureInfo("en-CA"));

            };
        }
        public void SetLocale(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

        }


    }
}