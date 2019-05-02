﻿using System;
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

namespace Mobility_Android.Activities
{
    [Activity(Label = "ConsolidationActivity", ParentActivity = typeof(HomeActivity))]
    public class ConsolidationActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmConsolidation);

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
                    Toast.MakeText(this, "Erreur, veuillez vérifier le numéro de licence et de palette", ToastLength.Long).Show();
                    IsBusy = false;
                }
                // Sinon on affiche la transaction de la licence
                else
                {
                    Toast.MakeText(this, "La licence :" + licence + " est dans " + destination, ToastLength.Long).Show();
                    IsBusy = false;
                    Finish();
                }
            };


        }
    }
}