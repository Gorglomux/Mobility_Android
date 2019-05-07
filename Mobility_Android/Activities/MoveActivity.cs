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
using Mobility_Android.Resources.global;
using Mobility_Android.WebService.Security;

namespace Mobility_Android.Activities
{
    [Activity(Label = "MoveActivity", ParentActivity = typeof(HomeActivity))]
    public class MoveActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmMove);

            translateScreen();

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

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<Button>(Resource.Id.btnRelocation).Text = Activities.ResourceFR.btnRelocation;
                        FindViewById<Button>(Resource.Id.btnConsolidate).Text = Activities.ResourceFR.btnConsolidate;
                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<Button>(Resource.Id.btnRelocation).Text = Activities.ResourceEN.btnRelocation;
                        FindViewById<Button>(Resource.Id.btnConsolidate).Text = Activities.ResourceEN.btnConsolidate;
                        break;
                    }
            }
        }

    }
}