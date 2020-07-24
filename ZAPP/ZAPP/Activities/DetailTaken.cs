using System;
using System.Collections;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;

namespace ZAPP
{
    [Activity(Label = "DetailTaken", NoHistory = true)]
    public class DetailTaken : Activity
    {   
        ArrayList taken;
        List<ListTaakRecord> taakRecords;
        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            DatabaseTaak dbt = new DatabaseTaak(this);
            taken = dbt.GetTakenByZorgmoment(Global.zorgmoment.id.ToString());
            taakRecords = new List<ListTaakRecord>();
            foreach (TaakRecord taak in taken)
            {
                ListTaakRecord row = new ListTaakRecord(taak.id.ToString(), taak.zorgmoment_id.ToString(), taak.stap.ToString(), taak.omschrijving, taak.voltooid.ToString());
                taakRecords.Add(row);
            }

            SetContentView(Resource.Layout.DetailTaken);
            ListView listview = FindViewById<ListView>(Resource.Id.Takenlijst);
            listview.Adapter = new DetailListViewAdapter(this, taakRecords);
            ListView aanwezigbtn = FindViewById<ListView>(Resource.Id.AanwezigButton);
            aanwezigbtn.Adapter = new PresentButtonAdapter(this);
            FindViewById<TextView>(Resource.Id.Opmerkingen).Text = Global.zorgmoment.opmerkingen;


            Button homebtn = FindViewById<Button>(Resource.Id.HomeButton);
            homebtn.Click += (object sender, EventArgs e) =>
            {
                StartActivity(typeof(Home));
            };
            Button adresbtn = FindViewById<Button>(Resource.Id.AdresButton);
            adresbtn.Click += (object sender, EventArgs e) =>
            {
                StartActivity(typeof(DetailAdres));
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