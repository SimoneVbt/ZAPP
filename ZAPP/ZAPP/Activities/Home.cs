﻿using System;
using System.Collections.Generic;
using ArrayList = System.Collections.ArrayList;
using Android.App;
using Android.OS;
using Android.Widget;

namespace ZAPP
{
    [Activity(Label = "Home")]
    public class Home : Activity
    {
        DatabaseZorgmoment dbz;
        ListView listview;
        List<ListZorgmomentRecord> momentRecords;
        ArrayList result;
        List<ListTaakRecord> taakRecords;
        ArrayList taken;
        Button logout;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            dbz = new DatabaseZorgmoment(this);
            result = dbz.GetAllZorgmomenten();
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

            logout = FindViewById<Button>(Resource.Id.Logout);
            logout.Click += Logout;
        }

        protected void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var moment = momentRecords[e.Position];
            ZorgmomentRecord moment_record = dbz.GetZorgmomentById(moment.id);
            Global.zorgmoment = moment_record;

            DatabaseClient dbc = new DatabaseClient(this);
            ClientRecord client = dbc.GetClientById(moment_record.client_id.ToString());
            Global.client = client;

            DatabaseTaak dbt = new DatabaseTaak(this);
            taken = dbt.GetTakenByZorgmoment(moment.id);
            taakRecords = new List<ListTaakRecord>();
            foreach (TaakRecord taak in taken)
            {
                ListTaakRecord row = new ListTaakRecord(taak.id.ToString(), taak.zorgmoment_id.ToString(), taak.stap.ToString(), taak.omschrijving, taak.voltooid.ToString());
                taakRecords.Add(row);
            }
            Global.taakRecords = taakRecords;

            StartActivity(typeof(DetailTaken));
        }

        public void Logout(object sender, EventArgs e)
        {
            DatabaseLogin dbl = new DatabaseLogin(this);
            dbl.Logout();
            StartActivity(typeof(Login));
        }
    }
}