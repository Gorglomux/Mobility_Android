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
    [Activity(Label = "ConsolidationActivity", ParentActivity = typeof(HomeActivity))]
    public class ConsolidationActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmConsolidation);

            translateScreen();

            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear), FindViewById<EditText>(Resource.Id.tfLicenseConsolidation));
            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear2), FindViewById<EditText>(Resource.Id.tfPalette));

            // Action clic sur confirmer pour mettre licence dans une nouvelle palette
            FindViewById<Button>(Resource.Id.btnConfirm).Click += async (sender, e) => {
                var licence = FindViewById<TextView>(Resource.Id.tfLicenseConsolidation).Text;
                var destination = FindViewById<TextView>(Resource.Id.tfPalette).Text;
                await Task.Delay(50);

                // Si il y a eu une erreur lors du transfère alors on affiche un message pour prévenir de l'erreur
                if (!(OperationsWebService.relocateLicense(Configuration.securityToken, licence, destination, RELOCATION_DESTINATION.Pallet, Configuration.userInfos.warehouseNRI)))
                {
                    switch (Configuration.currentLanguage)
                    {
                        case CR_TTLangue.French_Canada:
                            {
                                Toast.MakeText(this, "Erreur, veuillez vérifier le numéro de licence et de palette", ToastLength.Short).Show();
                                break;
                            }

                        case CR_TTLangue.English:
                            {
                                Toast.MakeText(this, "Error, please check the license and pallet number", ToastLength.Short).Show();
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
                        FindViewById<TextView>(Resource.Id.tvConsolidation).Text = Activities.ResourceFR.tvConsolidation;
                        FindViewById<TextView>(Resource.Id.tvLicense).Text = Activities.ResourceFR.tvLicense;
                        FindViewById<TextView>(Resource.Id.tvPalette).Text = Activities.ResourceFR.tvPalette;
                        FindViewById<Button>(Resource.Id.btnConfirm).Text = Activities.ResourceFR.btnConfirm;
                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<TextView>(Resource.Id.tvConsolidation).Text = Activities.ResourceEN.tvConsolidation;
                        FindViewById<TextView>(Resource.Id.tvLicense).Text = Activities.ResourceEN.tvLicense;
                        FindViewById<TextView>(Resource.Id.tvPalette).Text = Activities.ResourceEN.tvPalette;
                        FindViewById<Button>(Resource.Id.btnConfirm).Text = Activities.ResourceEN.btnConfirm;
                        break;
                    }
            }
        }
    }
}