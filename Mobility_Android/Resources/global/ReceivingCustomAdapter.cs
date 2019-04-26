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
    public class ReceivingCustomAdapter : BaseAdapter
    {
        private Activity activity;
        private List<ReceptionWS> receptions;

        public ReceivingCustomAdapter(Activity activity, List<ReceptionWS> receptions)
        {
            this.activity = activity;
            this.receptions = receptions;
        }

        public override int Count {
            get
            {
                return receptions.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return receptions[position].ReceptionNRI;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.listviewReceiving, parent, false);

            var tvNumRec = view.FindViewById<TextView>(Resource.Id.tvReception);
            var tvFournisseur = view.FindViewById<TextView>(Resource.Id.tvFournisseur);

            tvNumRec.Text = receptions[position].ReceptionNRI.ToString();
            tvFournisseur.Text = receptions[position].SupplierCode;

            return view;
        }
    }
}