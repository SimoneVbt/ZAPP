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
    [Activity(Label = "Home")]
    public class Home : Activity
    {
        ListView listview;
        List<ListZorgmomentRecord> records;
        ArrayList result;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var id = Intent.GetStringExtra("id");

            Database db = new Database(this);
            result = db.GetAllZorgmomenten();
            records = new List<ListZorgmomentRecord>();

            foreach (ZorgmomentRecord value in result)
            {
                ListZorgmomentRecord row = new ListZorgmomentRecord(value.id.ToString(), value.client_id.ToString(), Convert.ToString(value.datum_tijd), value.opmerkingen, Convert.ToString(value.aanwezigheid_begin), Convert.ToString(value.aanwezigheid_eind));
                records.Add(row);
            }

            SetContentView(Resource.Layout.Home);
            listview = FindViewById<ListView>(Resource.Id.ZorgmomentOverview);
            listview.Adapter = new HomeListViewAdapter(this, records);
            listview.ItemClick += OnListItemClick;
        }

        protected void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var moment = records[e.Position];
            var intent = new Intent(this, typeof(Takenlijst));
            intent.PutExtra("id", moment.id.ToString());
            StartActivityForResult(intent, 0);
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