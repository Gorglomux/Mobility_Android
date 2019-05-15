using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mobility_Android.Resources.global;
using Mobility_Android.Resources.webservice;
using Mobility_Android.WebService.Operations;
using Mobility_Android.WebService.Security;

namespace Mobility_Android.Activities
{
    [Activity(Label = "RelocationActivity", ParentActivity = typeof(HomeActivity))]
    public class RelocationActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmRelocation);

            translateScreen();

            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear), FindViewById<EditText>(Resource.Id.tfLicenseRelocation));
            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear2), FindViewById<EditText>(Resource.Id.tfDestination));

            // Action clic sur confirmer pour mettre licence dans une nouvelle destination
            FindViewById<Button>(Resource.Id.btnConfirm).Click += async (sender, e) => {
                var licence = FindViewById<TextView>(Resource.Id.tfLicenseRelocation).Text;
                var destination = FindViewById<TextView>(Resource.Id.tfDestination).Text;
                IsBusy = true;
                await Task.Delay(50);

                // Si il y a eu une erreur lors du transfère alors on affiche un message pour prévenir de l'erreur
                if (!(OperationsWebService.relocateLicense(Configuration.securityToken, licence, destination, RELOCATION_DESTINATION.Location, Configuration.userInfos.warehouseNRI)))
                {
                    switch (Configuration.currentLanguage)
                    {
                        case CR_TTLangue.French_Canada:
                            {
                                Toast.MakeText(this, "Erreur, veuillez vérifier le numéro de licence et de destination", ToastLength.Short).Show();
                                break;
                            }

                        case CR_TTLangue.English:
                            {
                                Toast.MakeText(this, "Error, please check the license and pallet destination", ToastLength.Short).Show();
                                break;
                            }
                    }

                    IsBusy = false;
                }
                // Sinon on affiche la transaction de la licence
                else
                {
                    switch (Configuration.currentLanguage)
                    {
                        case CR_TTLangue.French_Canada:
                            {
                                Toast.MakeText(this, "La licence :" + licence + " est dans " + destination, ToastLength.Long).Show();
                                break;
                            }

                        case CR_TTLangue.English:
                            {
                                Toast.MakeText(this, "The license :" + licence + " is now in" + destination, ToastLength.Long).Show();
                                break;
                            }
                    }
                    IsBusy = false;
                    Finish();
                }
            };
        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<TextView>(Resource.Id.tvDeplacement).Text = Activities.ResourceFR.tvDeplacement;
                        FindViewById<TextView>(Resource.Id.tvLicense).Text = Activities.ResourceFR.tvLicense;
                        FindViewById<TextView>(Resource.Id.tvDestination).Text = Activities.ResourceFR.tvDestination;

                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<TextView>(Resource.Id.tvDeplacement).Text = Activities.ResourceEN.tvDeplacement;
                        FindViewById<TextView>(Resource.Id.tvLicense).Text = Activities.ResourceEN.tvLicense;
                        FindViewById<TextView>(Resource.Id.tvDestination).Text = Activities.ResourceEN.tvDestination;
                        break;
                    }
            }
        }
    }
}