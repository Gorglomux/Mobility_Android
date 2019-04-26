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
    [Activity(Label = "CsDetailsActivity", ParentActivity = typeof(HomeActivity))]
    public class CsDetailsActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmCsDetails);

            ProductDetailsWS product = (ProductDetailsWS)ProductDetailsActivity.data;

            List<PickedLicensesWS> listProduct = product.pickedProducts.OfType<PickedLicensesWS>().ToList();

            ListView list = FindViewById<ListView>(Resource.Id.lvCSDetails);

            var adapter = new CsCustomAdapter(this, listProduct);
            list.Adapter = adapter;
        }
    }
}