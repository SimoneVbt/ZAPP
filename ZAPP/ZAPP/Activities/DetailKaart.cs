﻿using System;
using Android.App;
using Android.OS;
using Android.Widget;

namespace ZAPP
{
    [Activity(Label = "DetailKaart", NoHistory = true)]
    public class DetailKaart : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DetailKaart);
            ListView aanwezigbtn = FindViewById<ListView>(Resource.Id.AanwezigButton);
            aanwezigbtn.Adapter = new PresentButtonAdapter(this);

            Button homebtn = FindViewById<Button>(Resource.Id.HomeButton);
            homebtn.Click += Home;
            Button takenbtn = FindViewById<Button>(Resource.Id.TakenButton);
            takenbtn.Click += DetailTaken;
            Button adresbtn = FindViewById<Button>(Resource.Id.AdresButton);
            adresbtn.Click += DetailAdres;
        }

        public void Home(object sender, EventArgs e)
        {
            StartActivity(typeof(Home));
        }

        public void DetailTaken(object sender, EventArgs e)
        {
            StartActivity(typeof(DetailTaken));
            OverridePendingTransition(0, 0);
        }

        public void DetailAdres(object sender, EventArgs e)
        {
            StartActivity(typeof(DetailAdres));
            OverridePendingTransition(0, 0);
        }
    }
}