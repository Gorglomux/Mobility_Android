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
    public class CsCustomAdapter : BaseAdapter
    {
        private Activity activity;
        private List<PickedLicensesWS> listCs;

        public CsCustomAdapter(Activity activity, List<PickedLicensesWS> listCs)
        {
            
            this.activity = activity;

            /*int maxLength = 0;

            foreach (PickedLicensesWS licence in listCs)
            {
                if(maxLength < licence.code.Length)
                {
                    maxLength = licence.code.Length;
                }
            }

            foreach (PickedLicensesWS licence in listCs)
            {
                if(licence.code.Length < maxLength)
                {
                    int taille = maxLength - licence.code.Length;
                    for (int i=0; taille > i; i++)
                    {
                        licence.code += "   ";
                    }
                }
            }*/

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

            var tvCs = view.FindViewById<TextView>(Resource.Id.listCs);
            var tvWeight = view.FindViewById<TextView>(Resource.Id.tvWeight);

            tvCs.Text = listCs[position].code;
            tvWeight.Text = listCs[position].weight.ToString();


            return view;
        }
    }
}
