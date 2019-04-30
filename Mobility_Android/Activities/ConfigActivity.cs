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

            FindViewById<Button>(Resource.Id.btnSaveConfig).Click += (sender, e) =>
            {
                var textUrl = urlEditText.Text.Trim();
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

        public bool isValidURL(string url)
        {
            return Android.Util.Patterns.WebUrl.Matcher(url).Matches();
        }
    }
}