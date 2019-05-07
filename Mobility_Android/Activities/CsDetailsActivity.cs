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
using Mobility_Android.WebService.Operations;
using Mobility_Android.WebService.Security;

namespace Mobility_Android.Activities
{
    /** Appellée par ProductDetailActivity, cette activité sert a afficher le contenu d'un produit dans une réception
     *  
     * 
     * 
     **/
     
    public enum TYPE_CS { NONE, COMMANDE,RECEPTION};
    
    [Activity(Label = "CsDetailsActivity", ParentActivity = typeof(HomeActivity))]
    public class CsDetailsActivity : BaseActivity
    {
        public static TYPE_CS typeCS=TYPE_CS.NONE;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmCsDetails);

            translateScreen();

            List<PickedLicensesWS> listLicence = null;

            if (CsDetailsActivity.typeCS == TYPE_CS.RECEPTION)
            {
                // Récupération du produit sélectionné
                ProductDetailsWS product = (ProductDetailsWS)ProductDetailsActivity.data;

                // Récupération de la liste de licence d'un produit
                listLicence = product.pickedProducts.OfType<PickedLicensesWS>().ToList();
            }

            if (CsDetailsActivity.typeCS == TYPE_CS.COMMANDE)
            {
                // Récupération du produit sélectionné
                ProductDetailsWS product = (ProductDetailsWS)ProductDetailsActivity.data;

                // Récupération de la liste de licence d'un produit
                listLicence = product.pickedProducts.OfType<PickedLicensesWS>().ToList();
            }

            // Configuration de la ListView et de son Adapter par rapport à une liste de licence
            ListView list = FindViewById<ListView>(Resource.Id.lvCSDetails);
            var adapter = new CsCustomAdapter(this, listLicence);
            list.Adapter = adapter;

            // Si aucune licence alors on affiche un message pour prévenir l'utilisateur
            if (listLicence.Count == 0)
            {
                Toast.MakeText(this, "Pas de licence", ToastLength.Long).Show();
            }

            if (CsDetailsActivity.typeCS == TYPE_CS.NONE)
            {
                Toast.MakeText(this, "Erreur", ToastLength.Long).Show();
            }
        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<TextView>(Resource.Id.tvProductDetails).Text = Activities.ResourceFR.tvProductDetails;
                        FindViewById<TextView>(Resource.Id.tvCS).Text = Activities.ResourceFR.tvCS;
                        FindViewById<TextView>(Resource.Id.tvWeight).Text = Activities.ResourceFR.tvWeight;
                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<TextView>(Resource.Id.tvProductDetails).Text = Activities.ResourceEN.tvProductDetails;
                        FindViewById<TextView>(Resource.Id.tvCS).Text = Activities.ResourceEN.tvCS;
                        FindViewById<TextView>(Resource.Id.tvWeight).Text = Activities.ResourceEN.tvWeight;
                        break;
                    }
            }
        }
    }
}