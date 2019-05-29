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
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Mobility_Android.Resources.global;
using Mobility_Android.WebService.Security;

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

        private void OnPropertyChanged()
        {
            //Si l'application est occupée on affiche un message de chargement
            if (_isBusy == true)
            {
                ToastConfig toastConfig = null;
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_Canada:
                        {
                            toastConfig = new ToastConfig("Chargement...");
                            break;
                        }

                    case CR_TTLangue.English:
                        {
                            toastConfig = new ToastConfig("Loading...");
                            break;
                        }
                }
                
                toastConfig.SetDuration(500);
                toastConfig.SetBackgroundColor(System.Drawing.Color.FromArgb(12, 131, 193));
                UserDialogs.Instance.Toast(toastConfig);

            }
        }

        //Fonction appellée a la création de l'activité

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
                //Button backImage = FindViewById<Button>(Resource.Id.imBack);
                //On ferme la vue si le bouton est appuyé
                // backImage.Touch += (sender, e) =>
                // {  
                //    Finish();

                // };
                FindViewById<Button>(Resource.Id.imHome).Click += (sender, e) => {
                    Finish();
                    StartActivity(new Intent(this, typeof(HomeActivity)));
                };

            }

            SwipeRefreshLayout refresh= FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);

            if (refresh != null)
            {

                refresh.Refresh += async (sender, e) =>
                {
                    await Task.Delay(1000);

                    Recreate();
                };
            }
            SetActionBar(toolbar);
        }


        //Quand on appuie sur un bouton effacer (à coté des champs a renseigner dans la plupart des interfaces) l'EditText se vide
        public void clearTextOnClick(ImageButton clear, EditText et)
        {
            clear.Click += (sender,e)=>{
                et.Text = "";
            };
        }
    }
}