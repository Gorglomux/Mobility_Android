using System;
using System.Collections;
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

    [Activity(Label = "ReceivingActivity", ParentActivity = typeof(HomeActivity))]
    public class ReceivingActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmReceiving);


            List<ReceptionWS> receptions = OperationsWebService.getListReceptions(Configuration.securityToken);

            ListView list = FindViewById<ListView>(Resource.Id.lvReceiving);

            var adapter = new ReceivingCustomAdapter(this, receptions);
            list.Adapter = adapter;

            list.ItemClick += async (parent, args) =>
            {
                data = receptions[args.Position];
                IsBusy = true;
                await Task.Delay(50);
                StartActivity(new Intent(this, typeof(ReceivingDetailsActivity)));
                IsBusy = false;
            };
        }
    }
}