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

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            client = Global.client;

            SetContentView(Resource.Layout.DetailAdres);
            FindViewById<TextView>(Resource.Id.Adres).Text = $"{client.adres}";
            FindViewById<TextView>(Resource.Id.PostcodeWoonplaats).Text = $"{client.postcode} {client.woonplaats}";
            FindViewById<TextView>(Resource.Id.Telefoonnummer).Text = $" Telefoonnummer: {client.telefoonnummer}";

            ListView aanwezigbtn = FindViewById<ListView>(Resource.Id.AanwezigButton);
            aanwezigbtn.Adapter = new PresentButtonAdapter(this);

            Button homebtn = FindViewById<Button>(Resource.Id.HomeButton);
            homebtn.Click += Home;
            Button takenbtn = FindViewById<Button>(Resource.Id.TakenButton);
            takenbtn.Click += DetailTaken;
            Button kaartbtn = FindViewById<Button>(Resource.Id.KaartButton);
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