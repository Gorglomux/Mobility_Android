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
    [Activity(Label = "ConfigActivity", ParentActivity = typeof(HomeActivity))]
    public class ConfigActivity : BaseActivity
    {
        EditText urlEditText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState, Resource.Layout.frmConfig);

            urlEditText = FindViewById<EditText>(Resource.Id.tfUrl);
            urlEditText.AfterTextChanged += (sender, e) =>
            {
                Console.WriteLine("AHAHAHA");
            };
        }
    }
}