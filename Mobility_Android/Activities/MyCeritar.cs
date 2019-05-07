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
using Mobility_Android.WebService.Security;

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

            translateScreen();

            //On récupère le contenu des champs       
            EditText username = FindViewById<EditText>(Resource.Id.tfName);
            EditText password = FindViewById<EditText>(Resource.Id.tfPass);

            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear), username);
            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear2), password);

            FindViewById<RadioButton>(Resource.Id.rbFrench).Click += async (sender, e) =>
            {
                Configuration.currentLanguage = CR_TTLangue.French_Canada;
                Recreate();
            };

            FindViewById<RadioButton>(Resource.Id.rbEnglish).Click += async (sender, e) =>
            {
                Configuration.currentLanguage = CR_TTLangue.English;
                Recreate();
            };

            //Si l'utilisateur appuie sur le bouton connecter
            FindViewById<Button>(Resource.Id.btnConnect).Click += async  (sender, e) =>
            {
                //L'application est occupée
                IsBusy = true;
                await Task.Delay(50);
                //On essaye de se connecter au WebService
                if (SecurityWebService.doLogin(username.Text, password.Text))
                {
                    //Si la connection s'est bien passé on est envoyé vers la page d'accueil
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
        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                {
                    FindViewById<TextView>(Resource.Id.tvName).Text = Activities.ResourceFR.tvName;
                    FindViewById<TextView>(Resource.Id.tvPass).Text = Activities.ResourceFR.tvPass;
                    FindViewById<RadioButton>(Resource.Id.rbFrench).Text = Activities.ResourceFR.rbFrench;
                    FindViewById<RadioButton>(Resource.Id.rbEnglish).Text = Activities.ResourceFR.rbEnglish;
                    FindViewById<Button>(Resource.Id.btnConnect).Text = Activities.ResourceFR.btnConnect;
                    break;
                }

                case CR_TTLangue.English:
                { 
                    FindViewById<TextView>(Resource.Id.tvName).Text = Activities.ResourceEN.tvName;
                    FindViewById<TextView>(Resource.Id.tvPass).Text = Activities.ResourceEN.tvPass;
                    FindViewById<RadioButton>(Resource.Id.rbFrench).Text = Activities.ResourceEN.rbFrench;
                    FindViewById<RadioButton>(Resource.Id.rbEnglish).Text = Activities.ResourceEN.rbEnglish;
                    FindViewById<Button>(Resource.Id.btnConnect).Text = Activities.ResourceEN.btnConnect;
                     break;
                }
            }
        }

    }
}