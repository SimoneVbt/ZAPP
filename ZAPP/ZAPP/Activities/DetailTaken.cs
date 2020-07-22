using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Views;
using static Android.Widget.AdapterView;

namespace ZAPP
{
    [Activity(Label = "DetailTaken", NoHistory = true)]
    public class DetailTaken : Activity
    {
        ListView listview;
        List<ListTaakRecord> taakRecords;
        ZorgmomentRecord zorgmoment;
        Context context;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            taakRecords = Global.taakRecords;
            zorgmoment = Global.zorgmoment;

            SetContentView(Resource.Layout.DetailTaken);
            listview = FindViewById<ListView>(Resource.Id.Takenlijst);
            listview.Adapter = new DetailListViewAdapter(this, taakRecords);

            FindViewById<TextView>(Resource.Id.Opmerkingen).Text = zorgmoment.opmerkingen;
            //listview.ItemClick += CompletionChange;

            Button homebtn = FindViewById<Button>(Resource.Id.HomeButton);
            homebtn.Click += Home;
            Button adresbtn = FindViewById<Button>(Resource.Id.AdresButton);
            adresbtn.Click += DetailAdres;
            Button kaartbtn = FindViewById<Button>(Resource.Id.KaartButton);
            kaartbtn.Click += DetailKaart;
        }

        public void CompletionChange(object sender, ItemClickEventArgs e)
        {
            var taak = taakRecords[e.Position];
            View view = new View(context);

            TextView completion = view.FindViewById<TextView>(Resource.Id.TextRightBig);
            if (completion.Text == "V")
            {
                completion.Text = "O";
            }
            else
            {
                completion.Text = "V";
            }

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