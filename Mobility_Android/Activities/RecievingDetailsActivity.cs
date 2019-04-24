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
    [Activity(Label = "RecievingDetailsActivity", ParentActivity = typeof(HomeActivity))]
    public class RecievingDetailsActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmReceivingDetails);
            //Initialise les données des champs
            initChamps();

            //Listener a chaque entrée sur l'EditText
            //If license existe
                //Rajouter la license
            //Else
                //Créer la nouvelle license

            //Ajout listener Image details produits


            
        }

        private void initChamps()
        {
            //Initialisation des champs
        }
    }
}