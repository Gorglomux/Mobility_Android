using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Mobility_Android.WebService.Operations;
using ZXing.Mobile;

namespace Mobility_Android.Activities
{
    [Activity(Label = "RecievingDetailsActivity", ParentActivity = typeof(HomeActivity))]
    public class ReceivingDetailsActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmReceivingDetails);

            ReceptionWS reception = (ReceptionWS)ReceivingActivity.data;
            clearTextOnClick(FindViewById<ImageButton>(Resource.Id.imClear), FindViewById<EditText>(Resource.Id.tfLicenseReceivingDetails));
            FindViewById<TextView>(Resource.Id.tvNumRecieving).Text = reception.ReceptionNRI.ToString();
            FindViewById<TextView>(Resource.Id.tvnameProvider).Text = reception.SupplierCode;
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

            FindViewById<ImageButton>(Resource.Id.imDetails).Click += (sender, e) => {
                ReceptionWS data = reception;
                StartActivity(new Intent(this, typeof(ProductDetailsActivity)));
            };


            Toast.MakeText(this, "Réception : " + reception.ReceptionNRI,
            ToastLength.Long).Show();

        }


    }
}