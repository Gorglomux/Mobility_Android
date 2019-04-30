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

namespace Mobility_Android.Activities
{
    [Activity(Label = "ProductDetailsActivity", ParentActivity = typeof(HomeActivity))]
    public class ProductDetailsActivity : BaseActivity
    {
        protected override void  OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmProductDetails);

            ReceptionWS reception = (ReceptionWS)ReceivingDetailsActivity.data;

            List<ProductDetailsWS> listProduct = OperationsWebService.getReceptionProductDetails(Configuration.securityToken, reception.ReceptionNRI, (int)Configuration.currentLanguage, Configuration.userInfos.NRI, null).OfType<ProductDetailsWS>().ToList();

            ListView list = FindViewById<ListView>(Resource.Id.lvDetailsProduct);

            var adapter = new ProductCustomAdapter(this, listProduct);
            list.Adapter = adapter;

            list.ItemClick += (parent, args) =>
            {
                data = listProduct[args.Position];
                StartActivity(new Intent(this, typeof(CsDetailsActivity)));
            };
        }
    }
}