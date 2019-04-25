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
    [Activity(Label = "MoveActivity", ParentActivity = typeof(HomeActivity))]
    public class MoveActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmMove);

            //On stocke les boutons et les transitions dans un dictionnaire
            Dictionary<int, Intent> buttons = new Dictionary<int, Intent>()
            {
                {Resource.Id.btnConsolidate, new Intent(this, typeof(ConsolidationActivity))},
                {Resource.Id.btnRelocation, new Intent(this, typeof(RelocationActivity))}
                
            };


            //Pour chaque bouton on créer un évènement avec l'intention correspondante
            foreach (KeyValuePair<int, Intent> entry in buttons)
            {
                FindViewById<Button>(entry.Key).Click += (sender, e) =>
                {
                    StartActivity(entry.Value);
                };

            }

        }

    }
}