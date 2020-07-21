using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;

namespace ZAPP
{
    [Activity(Label = "DetailTaken", NoHistory = true)]
    public class DetailTaken : Activity
    {
        ListView listview;
        List<ListTaakRecord> taakRecords;
        ZorgmomentRecord zorgmoment;
        Button homebtn;
        Button adresbtn;
        Button kaartbtn;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            taakRecords = Global.taakRecords;
            zorgmoment = Global.zorgmoment;

            SetContentView(Resource.Layout.DetailTaken);
            listview = FindViewById<ListView>(Resource.Id.Takenlijst);
            listview.Adapter = new DetailListViewAdapter(this, taakRecords);

            FindViewById<TextView>(Resource.Id.Opmerkingen).Text = zorgmoment.opmerkingen;

            homebtn = FindViewById<Button>(Resource.Id.HomeButton);
            homebtn.Click += Home;
            adresbtn = FindViewById<Button>(Resource.Id.AdresButton);
            adresbtn.Click += DetailAdres;
            kaartbtn = FindViewById<Button>(Resource.Id.KaartButton);
            kaartbtn.Click += DetailKaart;
        }

        public void Home(object sender, EventArgs e)
        {
            StartActivity(typeof(Home));
        }

        public void DetailAdres(object sender, EventArgs e)
        {
            StartActivity(typeof(DetailAdres));
            OverridePendingTransition(0, 0);
        }

        public void DetailKaart(object sender, EventArgs e)
        {
            StartActivity(typeof(DetailKaart));
            OverridePendingTransition(0, 0);
        }

    }
}