﻿using System;
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
    public class CsCustomAdapter : BaseAdapter
    {
        private Activity activity;
        private List<PickedLicensesWS> listCs;

        public CsCustomAdapter(Activity activity, List<PickedLicensesWS> listCs)
        {
            this.activity = activity;
            this.listCs = listCs;
        }

        public override int Count
        {
            get
            {
                return listCs.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return listCs[position].nri;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.listviewCs, parent, false);

            var tvCs = view.FindViewById<TextView>(Resource.Id.tvCs);
            var tvWeight = view.FindViewById<TextView>(Resource.Id.tvWeight);

            tvCs.Text = listCs[position].code;
            tvWeight.Text = listCs[position].weight.ToString();

            return view;
        }
    }
}
