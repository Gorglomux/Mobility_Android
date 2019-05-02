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

namespace Mobility_Android.Activities
{
    /**
     * 
     * Créer une nouvelle commande 
     * 
     * */
    [Activity(Label = "NewSaleActivity", ParentActivity = typeof(HomeActivity))]
    public class NewSaleActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmNewSale);

            // Create your application here
        }
    }
}