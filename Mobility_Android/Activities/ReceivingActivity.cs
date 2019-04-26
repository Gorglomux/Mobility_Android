﻿using System;
using System.Collections;
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
using Mobility_Android.Resources.webservice;
using Mobility_Android.WebService.Operations;

namespace Mobility_Android.Activities
{

    [Activity(Label = "ReceivingActivity", ParentActivity = typeof(HomeActivity))]
    public class ReceivingActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmReceiving);

            // Récupération de la liste de réception selon l'utilisateur
            List<ReceptionWS> receptions = OperationsWebService.getListReceptions(Configuration.securityToken);

            // Configuration de la ListView et de son Adapter par rapport à une liste de réception
            ListView list = FindViewById<ListView>(Resource.Id.lvReceiving);
            var adapter = new ReceivingCustomAdapter(this, receptions);
            list.Adapter = adapter;

            // Action clic sur bouton pour accèder à une réception sélectionnée
            list.ItemClick += (parent, args) =>
            {
                // Sauvegarde de la réception choisie
                data = receptions[args.Position];
                StartActivity(new Intent(this, typeof(ReceivingDetailsActivity)));
            };

            // Si pas de reception alors message pour prévenir l'utilisateur
            if (receptions.Count == 0)
            {
                Toast.MakeText(this, "Pas de reception", ToastLength.Long).Show();
            }
        }
    }
}