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
    }
}