using System;
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

    /**
     *  Activité de base comprenant des fonctions génériques et dont toute les autres activité héritent.
     *  Gère les temps de chargement, la toolbar, la suppression de champs et le transfer des données entre plusieur vues
     * 
     * */
    [Activity(Label = "BaseActivity")]
    public abstract class BaseActivity : Activity
    {

        //Object permettant de stocker des données (pour l'utiliser à travers plusieurs vues

        public static object data = null;

        //Permet de déterminer si l'application doit afficher un message de chargement ou non

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                //Cette méthode est appellée quand on modifie le champ IsBusy
                OnPropertyChanged();
            }
        }

<<<<<<< HEAD
        private void OnPropertyChanged()
        {
            //Si l'application est occupée on affiche un message de chargement
            if (_isBusy == true)
            {
                var toastConfig = new ToastConfig("Chargement...");
                toastConfig.SetDuration(500);
                toastConfig.SetBackgroundColor(System.Drawing.Color.FromArgb(12, 131, 193));
                UserDialogs.Instance.Toast(toastConfig);

            }
        }

        //Fonction appellée a la création de l'activité
=======

>>>>>>> 78c5f6e45b45ac3b55f06b4c284ac28362714902
        protected void OnCreate(Bundle savedInstanceState, int layoutId)
        {
            base.OnCreate(savedInstanceState);
            //On affiche l'interface correspondante
            base.SetContentView(layoutId);

            //On active les UserDialogs (pour les messages de chargement)
            UserDialogs.Init(this);

            IsBusy = false;

            //Création de la toolbar
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            if (toolbar != null)
            {
                 //Interaction avec le bouton retour de la toolbar
                ImageView backImage = FindViewById<ImageView>(Resource.Id.imBack);
                //On ferme la vue si le bouton est appuyé
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

        //Fonction appelée quand on appuye sur le menu dans la toolbar
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);

            return base.OnCreateOptionsMenu(menu);
        }
        //Quand on appuie sur un élément du menu on change l'interface
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

            return base.OnOptionsItemSelected(item);
        }

        //Quand on appuie sur un bouton effacer (à coté des champs a renseigner dans la plupart des interfaces) l'EditText se vide
        public void clearTextOnClick(ImageButton clear, EditText et)
        {
            clear.Click += (sender,e)=>{
                et.Text = "";
            };
        }


<<<<<<< HEAD
=======
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

>>>>>>> 78c5f6e45b45ac3b55f06b4c284ac28362714902
    }
}