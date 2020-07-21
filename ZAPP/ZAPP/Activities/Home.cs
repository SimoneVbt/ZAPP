using System;
using System.Collections.Generic;
using ArrayList = System.Collections.ArrayList;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace ZAPP
{
    [Activity(Label = "Home")]
    public class Home : Activity
    {
        Database db;
        ListView listview;
        List<ListZorgmomentRecord> momentRecords;
        ArrayList result;
        List<ListTaakRecord> taakRecords;
        ArrayList taken;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            db = new Database(this);
            result = db.GetAllZorgmomenten();
            momentRecords = new List<ListZorgmomentRecord>();

            foreach (ZorgmomentRecord value in result)
            {
                ListZorgmomentRecord row = new ListZorgmomentRecord(value.id.ToString(), value.client_id.ToString(), Convert.ToString(value.datum_tijd), value.opmerkingen, Convert.ToString(value.aanwezigheid_begin), Convert.ToString(value.aanwezigheid_eind));
                momentRecords.Add(row);
            }

            SetContentView(Resource.Layout.Home);
            listview = FindViewById<ListView>(Resource.Id.ZorgmomentOverview);
            listview.Adapter = new HomeListViewAdapter(this, momentRecords);
            listview.ItemClick += OnListItemClick;
        }

        protected void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var moment = momentRecords[e.Position];

            ZorgmomentRecord moment_record = db.GetZorgmomentById(moment.id);
            GlobalZorgmoment.zorgmoment = moment_record;
            ClientRecord client = db.GetClientById(moment_record.client_id.ToString());
            GlobalClientRecord.client = client;

            taken = db.GetTakenByZorgmoment(moment.id);
            taakRecords = new List<ListTaakRecord>();
            foreach (TaakRecord taak in taken)
            {
                ListTaakRecord row = new ListTaakRecord(taak.id.ToString(), taak.zorgmoment_id.ToString(), taak.stap.ToString(), taak.omschrijving, taak.voltooid.ToString());
                taakRecords.Add(row);
            }
            GlobalTaakRecords.taakRecords = taakRecords;

            StartActivity(typeof(DetailTaken));
        }

        /*
         * public function ConvertDateTime()
         * {
         *  CultureInfo Nederlands = new CultureInfo("nl-NL", false);
            CultureInfo.CurrentCulture = Nederlands;
         * }
         */
    }
}