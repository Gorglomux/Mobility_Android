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
    public class ProductCustomAdapter : BaseAdapter
    {
        private Activity activity;
        private List<ProductDetailsWS> produits;

        public ProductCustomAdapter(Activity activity, List<ProductDetailsWS> produits)
        {
            this.activity = activity;
            this.produits = produits;
        }

        public override int Count
        {
            get
            {
                return produits.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return produits[position].NRI;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.listviewProduct, parent, false);

            var tvProductName = view.FindViewById<TextView>(Resource.Id.tvProductName);
            var tvQuantity = view.FindViewById<TextView>(Resource.Id.tvQuantity);

            tvProductName.Text = produits[position].code;
            tvQuantity.Text = produits[position].qtyPicked.ToString() + '/' +produits[position].qtyToPick.ToString();

            return view;
        }
    }
}
