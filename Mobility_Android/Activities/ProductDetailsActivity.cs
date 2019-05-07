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
using Mobility_Android.WebService.Security;

namespace Mobility_Android.Activities
{
    /**
     * 
     * Récupère la liste des produits d'une réception
     * */
    [Activity(Label = "ProductDetailsActivity", ParentActivity = typeof(HomeActivity))]
    public class ProductDetailsActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmProductDetails);

            translateScreen();

            List<ProductDetailsWS> listProduct =  null;

            if (CsDetailsActivity.typeCS == TYPE_CS.RECEPTION)
            {
                // Récupération de la réception sélectionnée
                ReceptionWS reception = (ReceptionWS)ReceivingDetailsActivity.data;
                // Récupération de la liste de produit selon une reception grâce au web service Operations
                listProduct = OperationsWebService.getReceptionProductDetails(Configuration.securityToken, reception.ReceptionNRI, (int)Configuration.currentLanguage, Configuration.userInfos.NRI, null).OfType<ProductDetailsWS>().ToList();
            }
            else if (CsDetailsActivity.typeCS == TYPE_CS.COMMANDE)
            {
                // Récupération de la réception sélectionnée
                SaleWS sale = (SaleWS)PickingListActivity.data;
                // Récupération de la liste de produit selon une reception grâce au web service Operations
                listProduct = OperationsWebService.getSaleProductDetails(Configuration.securityToken, sale.saleNRI, (int)Configuration.currentLanguage, Configuration.userInfos.NRI).OfType<ProductDetailsWS>().ToList();
            }

            // Configuration de la ListView et de son Adapter par rapport à une liste de produit
            ListView list = FindViewById<ListView>(Resource.Id.lvDetailsProduct);
            var adapter = new ProductCustomAdapter(this, listProduct);
            list.Adapter = adapter;

            // Action clic bouton pour sélectionner un produit
            list.ItemClick += (parent, args) =>
            {
                // Sauvegarde du produit sélectionné
                data = listProduct[args.Position];
                StartActivity(new Intent(this, typeof(CsDetailsActivity)));
            };

            // Si pas de liste de produit alors on previent l'utilisateur avec un msg
            if (listProduct.Count == 0)
            {
                Toast.MakeText(this, "Pas de produit", ToastLength.Long).Show();
            }
        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<TextView>(Resource.Id.tvProductDetails).Text = Activities.ResourceFR.tvProductDetails;
                        FindViewById<TextView>(Resource.Id.tvProductName).Text = Activities.ResourceFR.tvProductName;
                        FindViewById<TextView>(Resource.Id.tvQuantity).Text = Activities.ResourceFR.tvQuantity;
                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<TextView>(Resource.Id.tvProductDetails).Text = Activities.ResourceEN.tvProductDetails;
                        FindViewById<TextView>(Resource.Id.tvProductName).Text = Activities.ResourceEN.tvProductName;
                        FindViewById<TextView>(Resource.Id.tvQuantity).Text = Activities.ResourceEN.tvQuantity;
                        break;
                    }
            }
        }
    }
}