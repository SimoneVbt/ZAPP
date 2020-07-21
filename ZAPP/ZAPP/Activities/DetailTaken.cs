using System.Collections.Generic;
using ArrayList = System.Collections.ArrayList;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Views;
using System;

namespace ZAPP
{
    [Activity(Label = "DetailTaken", NoHistory = true)]
    public class DetailTaken : Activity
    {
        ListView listview;
        List<ListTaakRecord> taakRecords;
        Button homebtn;
        Button adresbtn;
        Button kaartbtn;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            taakRecords = GlobalTaakRecords.taakRecords;

            SetContentView(Resource.Layout.DetailTaken);
            listview = FindViewById<ListView>(Resource.Id.Takenlijst);
            listview.Adapter = new DetailListViewAdapter(this, taakRecords);

            homebtn = FindViewById<Button>(Resource.Id.HomeButton);
            homebtn.Click += Home;
            adresbtn = FindViewById<Button>(Resource.Id.AdresButton);
            adresbtn.Click += DetailAdres;
            kaartbtn = FindViewById<Button>(Resource.Id.KaartButton);
        }

        public void Home(object sender, EventArgs e)
        {
            StartActivity(typeof(Home));
        }

        public void DetailAdres(object sender, EventArgs e)
        {
            StartActivity(typeof(DetailAdres));
        }

    }
}