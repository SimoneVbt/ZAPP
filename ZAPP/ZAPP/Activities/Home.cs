using System;
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
        Button logout;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            dbz = new DatabaseZorgmoment(this);
            result = dbz.GetAllZorgmomenten();
            momentRecords = new List<ListZorgmomentRecord>();

            foreach (ZorgmomentRecord value in result)
            {
                ListZorgmomentRecord row = new ListZorgmomentRecord(value.id.ToString(), value.client_id.ToString(), Convert.ToString(value.datum_tijd), value.opmerkingen, Convert.ToString(value.aanwezigheid_begin), Convert.ToString(value.aanwezigheid_eind), value.nieuw.ToString());
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