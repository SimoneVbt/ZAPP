using System;
using Android.App;
using Android.OS;
using Android.Widget;

namespace ZAPP
{
    [Activity(Label = "DetailKaart", NoHistory = true)]
    public class DetailKaart : Activity
    {
        Button homebtn;
        Button takenbtn;
        Button adresbtn;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DetailKaart);

            homebtn = FindViewById<Button>(Resource.Id.HomeButton);
            homebtn.Click += Home;
            takenbtn = FindViewById<Button>(Resource.Id.TakenButton);
            takenbtn.Click += DetailTaken;
            adresbtn = FindViewById<Button>(Resource.Id.AdresButton);
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