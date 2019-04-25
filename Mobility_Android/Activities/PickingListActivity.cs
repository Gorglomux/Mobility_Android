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
    [Activity(Label = "PickingListActivity", ParentActivity = typeof(HomeActivity))]
    public class PickingListActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmPickingList);

            List<SaleWS> picking = OperationsWebService.(Configuration.securityToken);

            ListView list = FindViewById<ListView>(Resource.Id.lvPicking);

            var adapter = new ReceivingCustomAdapter(this, picking);
            list.Adapter = adapter;

            list.ItemClick += (parent, args) =>
            {
                data = picking[args.Position];

                ReceptionWS vente= (SaleWS)data;
                StartActivity(new Intent(this, typeof(ReceivingDetailsActivity)));
            };
        }
    }
}