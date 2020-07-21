using System;
using Android.App;
using Android.OS;
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
            client = Global.client;

            SetContentView(Resource.Layout.DetailAdres);
            FindViewById<TextView>(Resource.Id.Adres).Text = $"{client.adres}";
            FindViewById<TextView>(Resource.Id.PostcodeWoonplaats).Text = $"{client.postcode} {client.woonplaats}";
            FindViewById<TextView>(Resource.Id.Telefoonnummer).Text = $" Telefoonnummer: {client.telefoonnummer}";

            homebtn = FindViewById<Button>(Resource.Id.HomeButton);
            homebtn.Click += Home;
            takenbtn = FindViewById<Button>(Resource.Id.TakenButton);
            takenbtn.Click += DetailTaken;
            kaartbtn = FindViewById<Button>(Resource.Id.KaartButton);
            kaartbtn.Click += DetailKaart;
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

        public void DetailKaart(object sender, EventArgs e)
        {
            StartActivity(typeof(DetailKaart));
            OverridePendingTransition(0, 0);
        }
    }
}