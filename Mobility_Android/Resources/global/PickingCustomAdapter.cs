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
using Java.Lang;
using Mobility_Android.WebService.Operations;

namespace Mobility_Android.Resources.global
{

    public class PickingCustomAdapter : BaseAdapter
    {
        private Activity activity;
        private List<SaleWS> picking;

        public PickingCustomAdapter(Activity activity, List<SaleWS> picking)
        {
            this.activity = activity;
            this.picking = picking;
        }

        public override int Count
        {
            get
            {
                return picking.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return picking[position].saleNRI;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.listviewPicking, parent, false);

            var tvSale = view.FindViewById<TextView>(Resource.Id.tvSale);
            var tvCustomer = view.FindViewById<TextView>(Resource.Id.tvCustomer);

            tvSale.Text = picking[position].saleNRI.ToString();
            tvCustomer.Text = picking[position].customerCode;

            return view;
        }
    }
}