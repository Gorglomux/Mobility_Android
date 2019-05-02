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
using Mobility_Android.Resources.global;
using Mobility_Android.Resources.webservice;
using Mobility_Android.WebService.Operations;

namespace Mobility_Android.Activities
{
    /**
     * 
     * Activité permettant de créer une nouvelle license dans le cadre d'une réception
     * 
     * */
    [Activity(Label = "NewLicenseActivity", ParentActivity = typeof(HomeActivity))]
    public class NewLicenseActivity : BaseActivity
    {
        TextView dateSelect;
        Spinner spinner;
        LicenseWS licence = new LicenseWS();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmNewLicense);

            licence = ReceivingDetailsActivity.licence;
            var tfWeight = FindViewById<EditText>(Resource.Id.tfWeight);
            var tfQty = FindViewById<EditText>(Resource.Id.tfQty);

            // Récupération de la réception sélectionnée
            ReceptionWS reception = (ReceptionWS)ReceivingDetailsActivity.data;

            // Récupération de la liste de produit selon une reception grâce au web service Operations
            List<ProductDetailsWS> listProduct = OperationsWebService.getReceptionProductDetails(Configuration.securityToken, reception.ReceptionNRI, (int)Configuration.currentLanguage, Configuration.userInfos.NRI, null).OfType<ProductDetailsWS>().ToList();

            // Creation liste de nom produit pour le spinner
            List<string> typeProd = new List<string>();
            foreach (ProductDetailsWS p in listProduct)
            {
                if (!(typeProd.Contains(p.code.ToString())))
                {
                    typeProd.Add(p.code.ToString());
                } 
            }

            // Configuration du Spinner et de son Adapter par rapport à une liste de produit
            spinner = FindViewById<Spinner>(Resource.Id.spnProduct);
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, typeProd);
            spinner.Adapter = adapter;

            DateTime date = new DateTime();
            dateSelect = FindViewById<TextView>(Resource.Id.date_EditText);
            dateSelect.Click += (sender, e) =>
            {
                DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                {
                    date = time;
                    dateSelect.Text = time.ToLongDateString();
                    Console.Write(time.ToLongDateString());
                });
                Console.Write(dateSelect.Text);
                frag.Show(FragmentManager, DatePickerFragment.TAG);
            };

            spinner.ItemSelected += (parent, args) =>
            {
                // Sauvegarde de la réception choisie
                licence.productNRI = listProduct[args.Position].NRI;
                licence.productSSCC = listProduct[args.Position].SSCC[0];

            };

            FindViewById<Button>(Resource.Id.btnConfirm).Click += (sender, e) => {
                bool sucess = true;
                string msg = "Veuillez renseigner les champs :  ";

                if(date != null)
                {
                    licence.expirationDate = date;
                    licence.packingDate = date;
                    licence.saleDate = date;
                    licence.productionDate = date;
                } else
                {
                    msg += "date, ";
                    sucess = false;
                }

                if (licence.productNRI == 0)
                {
                    msg += "produit, ";
                    sucess = false;
                }

                if (tfWeight.Text != "")
                {
                    licence.weightKG = decimal.Parse(tfWeight.Text);
                } else
                {
                    msg += "poids, ";
                    sucess = false;
                }

                if (tfQty.Text != "")
                {
                    licence.qtyUdm = decimal.Parse(tfQty.Text);
                }
                else
                {
                    msg += "quantité, ";
                    sucess = false;
                }

                if (sucess == true)
                {
                    //OperationsWebService.pickLicenseReception(Configuration.securityToken, licence, (int)Configuration.currentLanguage, 0, "");
                    StartActivity(new Intent(this, typeof(ReceivingDetailsActivity)));
                    Finish();
                }
                else
                {
                    msg.Remove(msg.Length-2, 2);
                    Toast.MakeText(this,msg, ToastLength.Long).Show();
                }
            };
        }
    }
}