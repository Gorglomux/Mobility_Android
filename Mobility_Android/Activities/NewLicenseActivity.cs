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
using Mobility_Android.WebService.Security;

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
        DateTime? date;
        public static bool mustRefresh;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmNewLicense);

            translateScreen();

            mustRefresh = false;

            licence = ReceivingDetailsActivity.licence;
            var tfWeight = FindViewById<EditText>(Resource.Id.tfWeight);



            // Récupération de la réception sélectionnée
            ReceptionWS reception = (ReceptionWS)ReceivingDetailsActivity.data;

            // Récupération de la liste de produit selon une reception grâce au web service Operations
            List<ProductDetailsWS> listProduct = OperationsWebService.getReceptionProductDetails(Configuration.securityToken, reception.ReceptionNRI, (int)Configuration.currentLanguage, Configuration.userInfos.NRI, Configuration.userInfos.Udp_Label).OfType<ProductDetailsWS>().ToList();

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

            dateSelect = FindViewById<TextView>(Resource.Id.tvDate);
            dateSelect.Click += (sender, e) =>
            {
                DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                {
                    date = new DateTime();
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
                if (listProduct[args.Position].isFixedWeight)
                {
                    tfWeight.Text = listProduct[args.Position].defaultProductWeight.ToString();
                }

            };

            FindViewById<Button>(Resource.Id.btnConfirm).Click += async (sender, e) => {
                bool sucess = true;
                string msg = "";
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_Canada:
                        {
                            msg = "Veuillez renseigner les champs :  ";

                            break;
                        }

                    case CR_TTLangue.English:
                        {
                            msg = "Please fill in the fields:  ";
                            break;
                        }
                }
                
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
                    switch (Configuration.currentLanguage)
                    {
                        case CR_TTLangue.French_Canada:
                            {
                                msg += "produit, ";

                                break;
                            }

                        case CR_TTLangue.English:
                            {
                                msg += "product, ";
                                break;
                            }
                    }
                   
                    sucess = false;
                }

                if (tfWeight.Text != "")
                {
                    licence.weightKG = decimal.Parse(tfWeight.Text);
                    licence.weightLb = decimal.Parse(tfWeight.Text);
                } else
                {
                    switch (Configuration.currentLanguage)
                    {
                        case CR_TTLangue.French_Canada:
                            {
                                msg += "poids, ";

                                break;
                            }

                        case CR_TTLangue.English:
                            {
                                msg += "weight, ";
                                break;
                            }
                    }
                    sucess = false;
                }


                if (sucess == true)
                {
                    IsBusy = true;
                    await Task.Delay(50);
                    var produit = OperationsWebService.pickLicenseReception(Configuration.securityToken, licence, (int)Configuration.currentLanguage, Configuration.userInfos.Udp_NRI, Configuration.userInfos.Udp_Label);
                    if (produit ==null)
                    {
                        Toast.MakeText(this, OperationsWebService.errorMessage, ToastLength.Long).Show();
                    } else
                    {
                        if (ReceivingDetailsActivity.licences == null)
                        {
                            ReceivingDetailsActivity.licences = new List<LicenseWS>();
                        }
                        ReceivingDetailsActivity.licences.Add(licence);
                    }

                    IsBusy = false;
                    mustRefresh = true;
                    Finish();
                }
                else
                {
                    msg.Remove(msg.Length-2, 2);
                    Toast.MakeText(this,msg, ToastLength.Long).Show();
                }
            };
        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<TextView>(Resource.Id.lblTitleLicenseInfo).Text = Activities.ResourceFR.lblTitleLicenseInfo;
                        FindViewById<TextView>(Resource.Id.lblProduct).Text = Activities.ResourceFR.lblProduct;
                        FindViewById<TextView>(Resource.Id.lblDate).Text = Activities.ResourceFR.lblDate;
                        FindViewById<TextView>(Resource.Id.lblWeight).Text = Activities.ResourceFR.lblWeight;
                        FindViewById<Button>(Resource.Id.btnConfirm).Text = Activities.ResourceFR.btnConfirm;
                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<TextView>(Resource.Id.lblTitleLicenseInfo).Text = Activities.ResourceEN.lblTitleLicenseInfo;
                        FindViewById<TextView>(Resource.Id.lblProduct).Text = Activities.ResourceEN.lblProduct;
                        FindViewById<TextView>(Resource.Id.lblDate).Text = Activities.ResourceEN.lblDate;
                        FindViewById<TextView>(Resource.Id.lblWeight).Text = Activities.ResourceEN.lblWeight;
                        FindViewById<Button>(Resource.Id.btnConfirm).Text = Activities.ResourceEN.btnConfirm;
                        break;
                    }
            }
        }
    }
}