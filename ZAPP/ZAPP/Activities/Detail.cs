using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArrayList = System.Collections.ArrayList;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ZAPP
{
    [Activity(Label = "Takenlijst")]
    public class Takenlijst : Activity
    {
        ListView listview;
        List<ListTaakRecord> records;
        ArrayList result;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var moment_id = Intent.GetStringExtra("id");

            Database db = new Database(this);
            result = db.GetTakenByZorgmoment(moment_id);
            records = new List<ListTaakRecord>();
            
            foreach(TaakRecord value in result)
            {
                ListTaakRecord row = new ListTaakRecord(value.id.ToString(), value.zorgmoment_id.ToString(), value.stap.ToString(), value.omschrijving, value.voltooid.ToString());
                records.Add(row);
            }

            SetContentView(Resource.Layout.Detail);
            listview = FindViewById<ListView>(Resource.Id.Lijst);
            listview.Adapter = new TakenlijstListViewAdapter(this, records);
        }
    }
}