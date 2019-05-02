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
    [Activity(Label = "CsDetailsActivity", ParentActivity = typeof(HomeActivity))]
    public class CsDetailsActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmCsDetails);

            // Récupération du produit sélectionné
            ProductDetailsWS product = (ProductDetailsWS)ProductDetailsActivity.data;

            // Récupération de la liste de licence d'un produit
            List<PickedLicensesWS> listLicence = product.pickedProducts.OfType<PickedLicensesWS>().ToList();

            // Configuration de la ListView et de son Adapter par rapport à une liste de licence
            ListView list = FindViewById<ListView>(Resource.Id.lvCSDetails);
            var adapter = new CsCustomAdapter(this, listLicence);
            list.Adapter = adapter;

            // Si aucune licence alors on affiche un message pour prévenir l'utilisateur
            if (listLicence.Count == 0)
            {
                Toast.MakeText(this, "Pas de licence", ToastLength.Long).Show();
            }
        }
    }
}