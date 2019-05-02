using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    /** ConfigActivity
     * Est appelée quand l'utilisateur appuie sur l'image configuration sur le menu
     * Permet de modifier l'url du serveur 
     * Le serveur d'impression n'est pour l'instant pas géré
     * 
     * */
    [Activity(Label = "ConfigActivity", ParentActivity = typeof(HomeActivity))]
    public class ConfigActivity : BaseActivity
    {
        EditText urlEditText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmConfig);

            urlEditText = FindViewById<EditText>(Resource.Id.tfUrl);
            urlEditText.Text = Configuration.webServiceURL;
            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear), urlEditText);

            //Vérification du champ de l'url
            FindViewById<Button>(Resource.Id.btnSaveConfig).Click += (sender, e) =>
            {

                //On enlève les espaces en trop
                var textUrl = urlEditText.Text.Trim();
                //Si l'url est une url valide on regarde si il y a http devant, sinon on l'ajoute
                if (isValidURL(textUrl))
                {
                    
                    if (!textUrl.Contains("http://"))
                    {
                        Configuration.webServiceURL = "http://" + textUrl;
                    }
                    else
                    {
                        Configuration.webServiceURL = textUrl;
                    }

                    Toast.MakeText(this, "Configuration sauvegardée", ToastLength.Short).Show();

                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "URL invalide", ToastLength.Short).Show();
                }
            };
        }
        //Fonction permettant de tester si une url était valide
        public bool isValidURL(string url)
        {
            return Android.Util.Patterns.WebUrl.Matcher(url).Matches();
        }
    }
}