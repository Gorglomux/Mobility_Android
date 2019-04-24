using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Mobility_Android.global;

namespace Mobility_Android.Activities
{
    [Activity(Label = "NewSaleActivity", ParentActivity = typeof(HomeActivity))]
    public class NewSaleActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmNewSale);
            //Gestion du DatePicker
            TextView tvDateSale = FindViewById<TextView>(Resource.Id.tvDateSale);
            tvDateSale.Click += (sender, e) =>
            {
                
                DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                {
                    tvDateSale.Text = time.ToLongDateString();
                    
                });
                frag.Show(FragmentManager, DatePickerFragment.TAG);
                
            };
            

            //Création de la vente
            FindViewById<Button>(Resource.Id.btnConfirmSale).Click += (sender, e) =>
            {
                //Créer la vente
                Log.Debug("1",tvDateSale.Text);
                Finish();
            };
        }
    }
}