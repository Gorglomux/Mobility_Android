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

            // Action clic sur bouton pour completer une reception
            FindViewById<Button>(Resource.Id.btnEndReceiving).Click += (sender, e) => {
                Finish();
            };

            // Action clic sur bouton pour accèder aux details des produits
            FindViewById<ImageButton>(Resource.Id.imDetails).Click += (sender, e) => {
                // Sauvegarde de la réception
                data = reception;
                StartActivity(new Intent(this, typeof(ProductDetailsActivity)));
            };

            // Action touche "Enter" pour accèder à la création d'une nouvelle licence
            EditText urlEditText = FindViewById<EditText>(Resource.Id.tfLicenseReceivingDetails);  
            urlEditText.KeyPress += (object sender, View.KeyEventArgs e) => {
                e.Handled = false;
                if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
                {
                    if(urlEditText.Text.ToString() != "")
                    {
                        licence = new LicenseWS();
                        licence.licenseCode = urlEditText.Text.ToString();
                        licence.parentNRI = reception.ReceptionNRI;
                        data = reception;
                        StartActivity(new Intent(this, typeof(NewLicenseActivity)));
                        e.Handled = true;
                    } else
                    {
                        urlEditText.Text = "";
                        Toast.MakeText(this, "Veuillez entrer un code pour la licence", ToastLength.Long).Show();
                    }
                }
            };

            // Affichage du numéro de reception
            Toast.MakeText(this, "Réception : " + reception.ReceptionNRI, ToastLength.Long).Show();
        }
    }
}