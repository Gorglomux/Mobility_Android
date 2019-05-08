using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mobility_Android.Resources.global;
using Mobility_Android.Resources.webservice;
using Mobility_Android.WebService.Operations;
using Mobility_Android.WebService.Security;

namespace Mobility_Android.Activities
{

    [Activity(Label = "ReceivingActivity", ParentActivity = typeof(HomeActivity))]
    public class ReceivingActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmReceiving);

            translateScreen();

            CsDetailsActivity.typeCS = TYPE_CS.RECEPTION;

            // Récupération de la liste de réception selon l'utilisateur
            List<ReceptionWS> receptions = OperationsWebService.getListReceptions(Configuration.securityToken);

            // Configuration de la ListView et de son Adapter par rapport à une liste de réception
            ListView list = FindViewById<ListView>(Resource.Id.lvReceiving);

            var adapter = new ReceivingCustomAdapter(this, receptions);
            list.Adapter = adapter;

            list.ItemClick += async (parent, args) =>
            {
                // Sauvegarde de la réception choisie
                data = receptions[args.Position];
                IsBusy = true;
                await Task.Delay(50);
                StartActivity(new Intent(this, typeof(ReceivingDetailsActivity)));
                IsBusy = false;
            };

            // Si pas de reception alors message pour prévenir l'utilisateur
            if (receptions.Count == 0)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_Canada:
                        {
                            Toast.MakeText(this, "Pas de réception", ToastLength.Long).Show();

                            break;
                        }

                    case CR_TTLangue.English:
                        {
                            Toast.MakeText(this, "No reception", ToastLength.Long).Show();
                            break;
                        }
                }

            }

        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            if (hasFocus)
            {
                Recreate();
            }
        }

        private void translateScreen()
        {
            switch (Configuration.currentLanguage)
            {
                case CR_TTLangue.French_Canada:
                    {
                        FindViewById<TextView>(Resource.Id.tvReceiving).Text = Activities.ResourceFR.tvReceiving;
                        FindViewById<TextView>(Resource.Id.tvRec).Text = Activities.ResourceFR.tvRec;
                        FindViewById<TextView>(Resource.Id.tvFournisseur).Text = Activities.ResourceFR.tvFournisseur;

                        break;
                    }

                case CR_TTLangue.English:
                    {
                        FindViewById<TextView>(Resource.Id.tvReceiving).Text = Activities.ResourceEN.tvReceiving;
                        FindViewById<TextView>(Resource.Id.tvRec).Text = Activities.ResourceEN.tvRec;
                        FindViewById<TextView>(Resource.Id.tvFournisseur).Text = Activities.ResourceEN.tvFournisseur;
                        break;
                    }
            }
        }

    }
}