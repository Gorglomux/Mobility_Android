using System;
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
using Mobility_Android.WebService.Operations;
using ZXing.Mobile;
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


            FindViewById<ImageButton>(Resource.Id.imPhoto).Click += async (sender, e) =>
            {
                // Initialize the scanner first so it can track the current context
                MobileBarcodeScanner.Initialize(Application);

                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var result = await scanner.Scan();

                if (result != null)
                    Console.WriteLine("Scanned Barcode: " + result.Text);

            };

            FindViewById<ImageButton>(Resource.Id.imDetails).Click += async (sender, e) => {
                data = reception;
                IsBusy = true;
                await Task.Delay(50);
                StartActivity(new Intent(this, typeof(ProductDetailsActivity)));
                IsBusy = false;
            };

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

            // Affichage du numéro de reception
            Toast.MakeText(this, "Réception : " + reception.ReceptionNRI, ToastLength.Long).Show();
        }
    }
}