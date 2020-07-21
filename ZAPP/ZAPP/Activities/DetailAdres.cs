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

namespace ZAPP
{
    [Activity(Label = "DetailAdres", NoHistory = true)]
    public class DetailAdres : Activity
    {
        ClientRecord client;
        Button homebtn;
        Button takenbtn;
        Button kaartbtn;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            client = GlobalClientRecord.client;

            SetContentView(Resource.Layout.DetailAdres);

            homebtn = FindViewById<Button>(Resource.Id.HomeButton);
            homebtn.Click += Home;
            takenbtn = FindViewById<Button>(Resource.Id.TakenButton);
            takenbtn.Click += DetailTaken;
            kaartbtn = FindViewById<Button>(Resource.Id.KaartButton);
        }

        public void Home(object sender, EventArgs e)
        {
            StartActivity(typeof(Home));
        }

        public void DetailTaken(object sender, EventArgs e)
        {
            StartActivity(typeof(DetailTaken));
        }
    }
}