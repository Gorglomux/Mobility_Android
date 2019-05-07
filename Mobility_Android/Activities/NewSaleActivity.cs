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
    /**
     * 
     * Créer une nouvelle commande 
     * 
     * */
    [Activity(Label = "NewSaleActivity", ParentActivity = typeof(HomeActivity))]
    public class NewSaleActivity : BaseActivity
    {

        TextView dateSelect;
        Spinner spinnerClient;
        Spinner spinnerRecipient;
        Spinner spinnerOwner;
        public enum COMPANY_TYPE { CLIENT=1, TRANSPORTEUR=2, DESTINATAIRE=3, COMPTE_NATIONAL=4, MARQUE=5, FOURNISSEUR=6, ANY=999 };
        public static bool mustRefresh;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmNewSale);

            translateScreen();

            mustRefresh = false;

            spinnerClient = FindViewById<Spinner>(Resource.Id.spnClient);
            spinnerRecipient = FindViewById<Spinner>(Resource.Id.spnRecipient);
            spinnerOwner = FindViewById<Spinner>(Resource.Id.spnOwner);

            var po = FindViewById<EditText>(Resource.Id.tfPO);

            SaleObjectWS sale = new SaleObjectWS();

            // Creation des listes
            List<string> listClient = new List<string>();
            List<string> listRecipient = new List<string>();
            List<string> listOwner = new List<string>();

            var dictionaryClient = initSpinner(COMPANY_TYPE.CLIENT, spinnerClient, listClient);
            var dictionaryDestinataire = initSpinner(COMPANY_TYPE.DESTINATAIRE, spinnerRecipient, listRecipient);
            var dictionaryProprietaire = initSpinner(COMPANY_TYPE.ANY, spinnerOwner, listOwner);

            spinnerClient.ItemSelected += (parent, args) =>
            {
                sale.CustomerNRI = dictionaryClient.FirstOrDefault(x => x.Value == listClient[args.Position]).Key;
            };

            spinnerRecipient.ItemSelected += (parent, args) =>
            {
                sale.RecipientNRI = dictionaryDestinataire.FirstOrDefault(x => x.Value == listRecipient[args.Position]).Key;
            };

            spinnerOwner.ItemSelected += (parent, args) =>
            {
                sale.OwnerNRI = dictionaryProprietaire.FirstOrDefault(x => x.Value == listOwner[args.Position]).Key;
            };

            DateTime date = new DateTime();
            dateSelect = FindViewById<TextView>(Resource.Id.tvDate);
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


            FindViewById<Button>(Resource.Id.btnConfirm).Click += async (sender, e) =>
            {
                bool sucess = true;
                string msg = "Veuillez renseigner les champs :  ";

                sale.CreatorNRI = Configuration.userInfos.NRI;
                sale.WarehouseNRI = Configuration.userInfos.warehouseNRI;

                if (date != null)
                {
                    sale.DateCreated = DateTime.Today;
                    sale.DateExpected = date;
                }
                else
                {
                    msg += "date, ";
                    sucess = false;
                }

                if (po != null)
                {
                    sale.POCode = po.Text;
                }
                else
                {
                    msg += "PO, ";
                    sucess = false;
                }

                if (sucess == true)
                {
                    IsBusy = true;
                    await Task.Delay(50);

                    if(OperationsWebService.createSale(Configuration.securityToken, sale))
                    {
                        Toast.MakeText(this, "Vente créée", ToastLength.Long).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, OperationsWebService.errorMessage , ToastLength.Long).Show();
                    }
               
                    IsBusy = false;
                    mustRefresh = true;
                    Finish();
                }
                else
                {
                    msg.Remove(msg.Length - 2, 2);
                    Toast.MakeText(this, msg, ToastLength.Long).Show();
                }

                IsBusy = false;
            };
        }

        public Dictionary<int, string> initSpinner(COMPANY_TYPE company, Spinner spinner, List<string> list)
        {
            var dictionary = SecurityWebService.getListCompanies(Configuration.securityToken, (int)company);

            foreach (KeyValuePair<int, string> w in dictionary)
            {
                list.Add(w.Value);
            }

            // Configuration du Spinner et de son Adapter par rapport à une liste de produit
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, list);
            spinner.Adapter = adapter;

            return dictionary;
        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<TextView>(Resource.Id.lblTitleSaleInfo).Text = Activities.ResourceFR.lblTitleSaleInfo;
                        FindViewById<TextView>(Resource.Id.lblClient).Text = Activities.ResourceFR.lblClient;
                        FindViewById<TextView>(Resource.Id.lblRecipient).Text = Activities.ResourceFR.lblRecipient;
                        FindViewById<TextView>(Resource.Id.lblOwner).Text = Activities.ResourceFR.lblOwner;
                        FindViewById<TextView>(Resource.Id.lblPO).Text = Activities.ResourceFR.lblPO;
                        FindViewById<TextView>(Resource.Id.lblDateSale).Text = Activities.ResourceFR.lblDateSale;
                        FindViewById<Button>(Resource.Id.btnConfirm).Text = Activities.ResourceFR.btnConfirm;
                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<TextView>(Resource.Id.lblTitleSaleInfo).Text = Activities.ResourceEN.lblTitleSaleInfo;
                        FindViewById<TextView>(Resource.Id.lblClient).Text = Activities.ResourceEN.lblClient;
                        FindViewById<TextView>(Resource.Id.lblRecipient).Text = Activities.ResourceEN.lblRecipient;
                        FindViewById<TextView>(Resource.Id.lblOwner).Text = Activities.ResourceEN.lblOwner;
                        FindViewById<TextView>(Resource.Id.lblPO).Text = Activities.ResourceEN.lblPO;
                        FindViewById<TextView>(Resource.Id.lblDateSale).Text = Activities.ResourceEN.lblDateSale;
                        FindViewById<Button>(Resource.Id.btnConfirm).Text = Activities.ResourceEN.btnConfirm;
                        break;
                    }
            }
        }
    }
}