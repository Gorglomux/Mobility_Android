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
using Android.Util;
using System.Threading.Tasks;
using Mobility_Android.Resources.global;
using Mobility_Android.Resources.webservice;
using Mobility_Android.WebService.Security;

namespace Mobility_Android.Activities
{
    /**
     * 
     * Menu principal de l'application
     * Permet d'accéder a toute les fonctionnalités de l'application
     * 
     **/ 
    [Activity(Label = "HomeActivity", ParentActivity = typeof(MyCeritar))]
    public class HomeActivity : BaseActivity
    {
        Spinner spinner;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmHome);

            translateScreen();
            
            //On stocke les boutons et les transitions dans un dictionnaire
            Dictionary<int, Intent> buttons = new Dictionary<int, Intent>()
            {
                {Resource.Id.btnMove, new Intent(this, typeof(MoveActivity))},
                {Resource.Id.btnRecieve, new Intent(this, typeof(ReceivingActivity))},
                {Resource.Id.btnPicking, new Intent(this, typeof(PickingListActivity))},
             //   {Resource.Id.btnProduction, new Intent(this, typeof(ProductionMenuActivity))}
                
            };
            //{Resource.Id.btnInterWarehouse, new Intent(this, typeof(WarehouseListActivity)) }

            var dictionary = SecurityWebService.getListWarehouses(Configuration.securityToken);
            // Creation liste de nom produit pour le spinner
            List<string> warehouses = new List<string>();

            foreach (KeyValuePair<int, string> w in dictionary)
            {
                if(Configuration.userInfos.warehouseNRI == w.Key)
                {
                    //
                }
                warehouses.Add(w.Value);
            }

            // Configuration du Spinner et de son Adapter par rapport à une liste de produit
            spinner = FindViewById<Spinner>(Resource.Id.spnWarehouse);
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, warehouses);
            spinner.Adapter = adapter;

            spinner.ItemSelected += (parent, args) =>
            {
                Configuration.userInfos.warehouseNRI = dictionary.FirstOrDefault(x => x.Value == warehouses[args.Position]).Key;
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

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<TextView>(Resource.Id.tvWarehouse).Text = Activities.ResourceFR.tvWarehouse;
                        FindViewById<Button>(Resource.Id.btnMove).Text = Activities.ResourceFR.btnMove;
                        FindViewById<Button>(Resource.Id.btnRecieve).Text = Activities.ResourceFR.btnRecieve;
                        FindViewById<Button>(Resource.Id.btnPicking).Text = Activities.ResourceFR.btnPicking;
                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<TextView>(Resource.Id.tvWarehouse).Text = Activities.ResourceEN.tvWarehouse;
                        FindViewById<Button>(Resource.Id.btnMove).Text = Activities.ResourceEN.btnMove;
                        FindViewById<Button>(Resource.Id.btnRecieve).Text = Activities.ResourceEN.btnRecieve;
                        FindViewById<Button>(Resource.Id.btnPicking).Text = Activities.ResourceEN.btnPicking;
                        break;
                    }
            }
        }

    }
}