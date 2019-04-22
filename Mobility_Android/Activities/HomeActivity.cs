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
using Android.Util;

namespace Mobility_Android.Activities
{
    [Activity(Label = "HomeActivity", ParentActivity = typeof(LoginActivity))]
    public class HomeActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmHome);
            
            //On stocke les boutons et les transitions dans un dictionnaire
            Dictionary<int, Intent> buttons = new Dictionary<int, Intent>()
            {
                {Resource.Id.btnMove, new Intent(this, typeof(MoveActivity))},
                {Resource.Id.btnRecieve, new Intent(this, typeof(ReceivingActivity))},
                {Resource.Id.btnPicking, new Intent(this, typeof(PickingListActivity))},
                {Resource.Id.btnProduction, new Intent(this, typeof(ProductionMenuActivity))},
                {Resource.Id.btnInterWarehouse, new Intent(this, typeof(WarehouseListActivity)) }
            };


            //Pour chaque bouton on créer un évènement avec l'intention correspondante
            foreach (KeyValuePair< int, Intent> entry in buttons)
            {
                FindViewById<Button>(entry.Key).Click += (sender, e) =>
                {
                    StartActivity(entry.Value);
                };

            }
            
        }
    }
}