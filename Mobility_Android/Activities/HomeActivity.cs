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
using System.Threading.Tasks;
using Mobility_Android.Resources.global;
using Mobility_Android.Resources.webservice;

namespace Mobility_Android.Activities
{
    /**
     * 
     * Menu principal de l'application
     * Permet d'accéder a toute les fonctionnalités de l'application
     * 
     **/ 
    [Activity(Label = "HomeActivity", ParentActivity = typeof(LoginActivity))]
    public class HomeActivity : BaseActivity
    {
        Spinner spinner;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmHome);
            
            //On stocke les boutons et les transitions dans un dictionnaire
            Dictionary<int, Intent> buttons = new Dictionary<int, Intent>()
            {
                {Resource.Id.btnMove, new Intent(this, typeof(MoveActivity))},
                {Resource.Id.btnRecieve, new Intent(this, typeof(ReceivingActivity))},
                {Resource.Id.btnPicking, new Intent(this, typeof(PickingListActivity))},
                {Resource.Id.btnProduction, new Intent(this, typeof(ProductionMenuActivity))}
                
            };
            //{Resource.Id.btnInterWarehouse, new Intent(this, typeof(WarehouseListActivity)) }

            var dictionary = SecurityWebService.getListWarehouses(Configuration.securityToken);
            // Creation liste de nom produit pour le spinner
            List<string> warehouses = new List<string>();
            foreach (KeyValuePair<int, string> w in dictionary)
            {
                if(Configuration.userInfos.warehouseNRI == w.Key)
                {

                }
                warehouses.Add(w.Value);
            }

            // Configuration du Spinner et de son Adapter par rapport à une liste de produit
            spinner = FindViewById<Spinner>(Resource.Id.spnWarehouse);
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, warehouses);
            spinner.Adapter = adapter;

            spinner.ItemSelected += (parent, args) =>
            {
                foreach (KeyValuePair<int, string> w in dictionary)
                {
                    if (warehouses[args.Position] == w.Value)
                    {
                        Configuration.userInfos.warehouseNRI = w.Key;
                    }
                }
            };

            //Pour chaque bouton on créer un évènement avec l'intention correspondante
            foreach (KeyValuePair< int, Intent> entry in buttons)
            {
                FindViewById<Button>(entry.Key).Click += async(sender, e) =>
                {
                    IsBusy = true;
                    await Task.Delay(50);
                    StartActivity(entry.Value);
                    IsBusy = false;
                };

            }
            
        }

    }
}