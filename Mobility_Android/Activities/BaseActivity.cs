﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

// Article sur la création d'une activité de base https://stackoverflow.com/questions/38435138/set-toolbar-for-all-activities
// Article (en Java) http://mateoj.com/2015/06/21/adding-toolbar-and-navigation-drawer-all-activities-android/

namespace Mobility_Android.Activities
{
    [Activity(Label = "BaseActivity")]
    public abstract class BaseActivity : Activity
    {
        public static object data=null;
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }


        protected void OnCreate(Bundle savedInstanceState, int layoutId)
        {
            base.OnCreate(savedInstanceState);
            base.SetContentView(layoutId);
            UserDialogs.Init(this);
            IsBusy = false;
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            if (toolbar != null)
            {
                 
                ImageView backImage = FindViewById<ImageView>(Resource.Id.imBack);
                backImage.Touch += (sender, e) =>
                {

                    Finish();

                };


            }

            ImageButton refreshImage = FindViewById<ImageButton>(Resource.Id.imRefresh);

            if (refreshImage != null)
            {

                refreshImage.Touch += (sender, e) =>
                {
                    
                    refresh();
                };
            }
            SetActionBar(toolbar);
            
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);

            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intentMenu= new Intent(this, typeof(HomeActivity));
            switch (item.ItemId)
            {
                case Resource.Id.menuHome:
                    intentMenu = new Intent(this, typeof(HomeActivity));
                    break;
                case Resource.Id.menuMove:
                    intentMenu = new Intent(this, typeof(MoveActivity));
                    break;
                case Resource.Id.menuPicking:
                    intentMenu = new Intent(this, typeof(PickingListActivity));
                    break;
                case Resource.Id.menuProduction:
                    intentMenu = new Intent(this, typeof(ProductionMenuActivity));
                    break;
                case Resource.Id.menuRecieving:
                    intentMenu = new Intent(this, typeof(ReceivingActivity));
                    break;
                /*case Resource.Id.menuWarehouse:
                    intentMenu = new Intent(this, typeof(WarehouseListActivity));
                    break;
                    */
            }
            StartActivity(intentMenu);


            Toast.MakeText(this, "Action selected: " + item.ItemId,
                ToastLength.Long).Show();
            return base.OnOptionsItemSelected(item);
        }

        public void clearTextOnClick(ImageButton clear, EditText et)
        {
            clear.Click += (sender,e)=>{
                et.Text = "";
            };
        }


        private void OnPropertyChanged()
        {
            if (_isBusy)
            {
                var toastConfig = new ToastConfig("Chargement...");
                toastConfig.SetDuration(500);
                toastConfig.SetBackgroundColor(System.Drawing.Color.FromArgb(12, 131, 193));
                UserDialogs.Instance.Toast(toastConfig);
                
            }
        }
        public void refresh()
        {
            Recreate();
        }

    }
}