using System;
using Android.App;
using Android.OS;
using Android.Widget;

namespace ZAPP
{
    [Activity(Label = "DetailAdres", NoHistory = true)]
    public class DetailAdres : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            DatabaseClient dbc = new DatabaseClient(this);
            ClientRecord client = dbc.GetClientById(Global.zorgmoment.client_id.ToString());

            SetContentView(Resource.Layout.DetailAdres);
            FindViewById<TextView>(Resource.Id.Naam).Text = $"{client.voornaam} {client.achternaam}";
            FindViewById<TextView>(Resource.Id.Adres).Text = $"{client.adres}";
            FindViewById<TextView>(Resource.Id.PostcodeWoonplaats).Text = $"{client.postcode} {client.woonplaats}";
            FindViewById<TextView>(Resource.Id.Telefoonnummer).Text = $" Telefoonnummer: {client.telefoonnummer}";
            FindViewById<TextView>(Resource.Id.Opmerkingen).Text = Global.zorgmoment.opmerkingen;

            ListView aanwezigbtn = FindViewById<ListView>(Resource.Id.AanwezigButton);
            aanwezigbtn.Adapter = new PresentButtonAdapter(this);


            Button homebtn = FindViewById<Button>(Resource.Id.HomeButton);
            homebtn.Click += (object sender, EventArgs e) =>
            {
                StartActivity(typeof(Home));
            };
            Button takenbtn = FindViewById<Button>(Resource.Id.TakenButton);
            takenbtn.Click += (object sender, EventArgs e) =>
            {
                StartActivity(typeof(DetailTaken));
                OverridePendingTransition(0, 0);
            };
            Button kaartbtn = FindViewById<Button>(Resource.Id.KaartButton);
            kaartbtn.Click += (object sender, EventArgs e) =>
            {
                StartActivity(typeof(DetailKaart));
                OverridePendingTransition(0, 0);
            };
        }
    }
}