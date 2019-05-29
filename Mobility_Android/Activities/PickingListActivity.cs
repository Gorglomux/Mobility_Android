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
     * Page avec la liste de toutes les commandes avec possibilté d'en créer une nouvelle
     * 
     * */
     
    [Activity(Label = "PickingListActivity", ParentActivity = typeof(HomeActivity))]
    public class PickingListActivity : BaseActivity
    {
        public static object data = null;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmPickingList);

            translateScreen();

            CsDetailsActivity.typeCS = TYPE_CS.COMMANDE;

            CR_ResultActionOfListOfSaleWS result = OperationsWebService.getListPickingSale(Configuration.securityToken, (int)Configuration.currentLanguage, Configuration.userInfos.warehouseNRI);

            List<SaleWS> picking = result.ReturnValue.OfType<SaleWS>().ToList();

            ListView list = FindViewById<ListView>(Resource.Id.lvPicking);

            var adapter = new PickingCustomAdapter(this, picking);
            list.Adapter = adapter;

            // Action clic sur ajouter pour accèder à la liste de produit d'une reception
            FindViewById<Button>(Resource.Id.imNewSale).Click += async (sender, e) => {
                IsBusy = true;
                await Task.Delay(50);
                StartActivity(new Intent(this, typeof(NewSaleActivity)));
                IsBusy = false;
            };

            list.ItemClick += (parent, args) =>
            {
                data = picking[args.Position];
                StartActivity(new Intent(this, typeof(PickingDetailsActivity)));
            };
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            if (hasFocus && (NewSaleActivity.mustRefresh|PickingDetailsActivity.mustRefresh))
            {
                NewSaleActivity.mustRefresh = false;
                PickingDetailsActivity.mustRefresh = false;
                Recreate();
            }

        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<TextView>(Resource.Id.tvTitlePicking).Text = Activities.ResourceFR.tvTitlePicking;
                        FindViewById<TextView>(Resource.Id.tvSale).Text = Activities.ResourceFR.tvSale;
                        FindViewById<TextView>(Resource.Id.tvCustomer).Text = Activities.ResourceFR.tvCustomer;
                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<TextView>(Resource.Id.tvTitlePicking).Text = Activities.ResourceEN.tvTitlePicking;
                        FindViewById<TextView>(Resource.Id.tvSale).Text = Activities.ResourceEN.tvSale;
                        FindViewById<TextView>(Resource.Id.tvCustomer).Text = Activities.ResourceEN.tvCustomer;
                        break;
                    }
            }
        }
    }
}