﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Mobility_Android.Resources.global;
using Mobility_Android.Resources.webservice;
using Mobility_Android.WebService.Operations;
using static Android.InputMethodServices.KeyboardView;

namespace Mobility_Android.Activities
{
    [Activity(Label = "RecievingDetailsActivity", ParentActivity = typeof(HomeActivity))]
    public class ReceivingDetailsActivity : BaseActivity
    {
        public static LicenseWS licence;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmReceivingDetails);

            // Récupération de la réception
            ReceptionWS reception = (ReceptionWS)ReceivingActivity.data;

            // Action clic pour clear le EditText
            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear), FindViewById<EditText>(Resource.Id.tfLicenseReceivingDetails));

            // Remplir champs de données par rapport à la réception
            FindViewById<TextView>(Resource.Id.tvNumRecieving).Text = reception.ReceptionNRI.ToString();
            FindViewById<TextView>(Resource.Id.tvnameProvider).Text = reception.SupplierCode;

            // Affichage de la dernière licence créer, si pas de licence alors on n'affiche rien
            if (licence!=null)
            {
                if(licence.productNRI != 0)
                {
                    // Creation liste de nom produit
                    List<ProductDetailsWS> listProduct = OperationsWebService.getReceptionProductDetails(Configuration.securityToken, reception.ReceptionNRI, (int)Configuration.currentLanguage, Configuration.userInfos.NRI, null).OfType<ProductDetailsWS>().ToList();

                    // On parcourt la liste de produit pour trouver le produit qui correspond à la licence
                    foreach (ProductDetailsWS p in listProduct)
                    {
                        // Puis on affichage les information dans les TextView
                        if (licence.productNRI == p.NRI)
                        {
                            FindViewById<TextView>(Resource.Id.tvNameProduct).Text = p.code;
                            FindViewById<TextView>(Resource.Id.tvAmountQte).Text = p.qtyPicked.ToString() + "/" + p.qtyToPick.ToString();
                            FindViewById<TextView>(Resource.Id.tvAmountPoids).Text = licence.weightKG.ToString() + " kg";
                        }
                    }
                }
                

            }

            // Action touche "Enter" pour accèder à la création d'une nouvelle licence
            EditText urlEditText = FindViewById<EditText>(Resource.Id.tfLicenseReceivingDetails);
            urlEditText.KeyPress += (object sender, View.KeyEventArgs e) => {
                e.Handled = false;
                if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
                {
                    if (urlEditText.Text.ToString() != "")
                    {
                        licence = new LicenseWS();
                        licence.licenseCode = urlEditText.Text.ToString();
                        licence.parentNRI = reception.ReceptionNRI;
                        data = reception;
                        StartActivity(new Intent(this, typeof(NewLicenseActivity)));
                        e.Handled = true;
                    }
                    else
                    {
                        urlEditText.Text = "";
                        Toast.MakeText(this, "Veuillez entrer un code pour la licence", ToastLength.Long).Show();
                    }
                }
            };

            // Action clic sur détails pour accèder à la liste de produit d'une reception
            FindViewById<ImageButton>(Resource.Id.imDetails).Click += async (sender, e) => {
                data = reception;
                IsBusy = true;
                await Task.Delay(50);
                StartActivity(new Intent(this, typeof(ProductDetailsActivity)));
                IsBusy = false;
            };

            // Action clic sur bouton pour completer une reception
            FindViewById<Button>(Resource.Id.btnEndReceiving).Click += async(sender, e) => {
                IsBusy = true;
                await Task.Delay(50);
                OperationsWebService.completeReception(Configuration.securityToken, reception.ReceptionNRI);
                IsBusy = false;

                
                Finish();
            };
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            if (hasFocus && NewLicenseActivity.mustRefresh)
            {
                NewLicenseActivity.mustRefresh = false;
                Recreate();
            }
            
        }
    }
}