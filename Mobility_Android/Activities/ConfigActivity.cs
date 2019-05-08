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

            translateScreen();

            //Obtient le chemin jusqu'au dossier "fichiers", où l'on peut écrire et lire des fichiers
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            //Si l'utilisateur a deja sauvegardé une autre URL que celle de base, alors on lit le fichier la contenant; sinon, on crée ce dernier avec une URL de base
            var filePath = System.IO.Path.Combine(documentsPath, "WebServiceURL.txt");
            if (System.IO.File.Exists(filePath))
            {
                Configuration.webServiceURL = System.IO.File.ReadAllText(filePath);
            }
            else
            {
                System.IO.File.WriteAllText(filePath, Configuration.webServiceURL);
            }

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

                    Toast.MakeText(this, "Configuration sauvegardée dans" + filePath, ToastLength.Short).Show();

                    switch (Configuration.currentLanguage)
                    {
                        case CR_TTLangue.French_Canada:
                            {
                                Toast.MakeText(this, "Configuration sauvegardée", ToastLength.Short).Show();
                                break;
                            }

                        case CR_TTLangue.English:
                            {
                                Toast.MakeText(this, "Configuration saved", ToastLength.Short).Show();
                                break;
                            }
                    }
                    
                    //Sauvegarde l'URL dans le fichier "WebServiceURL.txt"
                    System.IO.File.WriteAllText(filePath, Configuration.webServiceURL);

                    Finish();
                }
                else
                {
                    switch (Configuration.currentLanguage)
                    {
                        case CR_TTLangue.French_Canada:
                            {
                                Toast.MakeText(this, "URL invalide", ToastLength.Short).Show();
                                break;
                            }

                        case CR_TTLangue.English:
                            {
                                Toast.MakeText(this, "Invalid URL", ToastLength.Short).Show();
                                break;
                            }
                    }
                    
                }
            };
        }
        //Fonction permettant de tester si une url était valide
        public bool isValidURL(string url)
        {
            return Android.Util.Patterns.WebUrl.Matcher(url).Matches();
        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<TextView>(Resource.Id.tvConfig).Text = Activities.ResourceFR.tvConfig;
                        FindViewById<TextView>(Resource.Id.tvOrder).Text = Activities.ResourceFR.tvOrder;
                        FindViewById<TextView>(Resource.Id.tvTermicon).Text = Activities.ResourceFR.tvTermicon;
                        FindViewById<TextView>(Resource.Id.tvIp).Text = Activities.ResourceFR.tvIp;
                        FindViewById<TextView>(Resource.Id.tvPort).Text = Activities.ResourceFR.tvPort;
                        FindViewById<Button>(Resource.Id.btnSaveConfig).Text = Activities.ResourceFR.btnSaveConfig;
                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<TextView>(Resource.Id.tvConfig).Text = Activities.ResourceEN.tvConfig;
                        FindViewById<TextView>(Resource.Id.tvOrder).Text = Activities.ResourceEN.tvOrder;
                        FindViewById<TextView>(Resource.Id.tvTermicon).Text = Activities.ResourceEN.tvTermicon;
                        FindViewById<TextView>(Resource.Id.tvIp).Text = Activities.ResourceEN.tvIp;
                        FindViewById<TextView>(Resource.Id.tvPort).Text = Activities.ResourceEN.tvPort;
                        FindViewById<Button>(Resource.Id.btnSaveConfig).Text = Activities.ResourceEN.btnSaveConfig;
                        break;
                    }
            }
        }
    }
}