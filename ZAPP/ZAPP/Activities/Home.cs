using System;
using System.Collections;
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
    [Activity(Label = "Home")]
    public class Home : Activity
    {
        ListView listview;
        List<ListZorgmomentRecord> records;
        ArrayList result;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Database db = new Database(this);
            result = db.GetAllZorgmomenten();
            records = new List<ListZorgmomentRecord>();

            foreach (ZorgmomentRecord value in result)
            {
                ListZorgmomentRecord row = new ListZorgmomentRecord(value.id.ToString(), value.client_id.ToString(), value.datum_tijd, value.opmerkingen, value.aanwezigheid_begin, value.aanwezigheid_eind);
                records.Add(row);
            }

            SetContentView(Resource.Layout.Home);
            listview = FindViewById<ListView>(Resource.Id.ZorgmomentOverview);
            listview.Adapter = new HomeListViewAdapter(this, records);
        }
    }
}