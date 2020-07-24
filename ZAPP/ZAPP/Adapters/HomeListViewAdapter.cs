using System;
using System.Collections.Generic;
using System.Globalization;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;

namespace ZAPP
{
    [Activity(Label = "HomeListViewAdapter")]
    public class HomeListViewAdapter : BaseAdapter<ListZorgmomentRecord>
    {

        readonly List<ListZorgmomentRecord> momenten;
        readonly Activity context;

        public HomeListViewAdapter(Activity context, List<ListZorgmomentRecord> momenten)
            : base()
        {
            this.context = context;
            this.momenten = momenten;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var moment = momenten[position];
            DatabaseClient dbc = new DatabaseClient(context);
            var client = dbc.GetClientById(moment.client_id);

            View view = convertView;

            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.ListRow, parent, false);
            }

            CultureInfo Nederlands = new CultureInfo("nl-NL", false);
            CultureInfo.CurrentCulture = Nederlands;
            DateTime date = Convert.ToDateTime(moment.datum_tijd);
            string dateString = date.ToString("dd-MM-yyyy");
            string timeString = date.ToString("HH:mm");

            view.FindViewById<TextView>(Resource.Id.TextLeftBig).Text = $"{client.voornaam} {client.achternaam}";
            view.FindViewById<TextView>(Resource.Id.TextLeftSmall).Text = $"{client.adres}, {client.postcode} {client.woonplaats}";
            view.FindViewById<TextView>(Resource.Id.TextRightBig).Text = dateString;
            view.FindViewById<TextView>(Resource.Id.TextRightSmall).Text = timeString;

            if (moment.nieuw == "1")
            {
                LinearLayout row = view.FindViewById<LinearLayout>(Resource.Id.ListViewRow);
                row.SetBackgroundColor(Color.ParseColor("#FF6eC7"));
            }

            return view;
        }

        public override ListZorgmomentRecord this[int position]
        {
            get { return momenten[position]; }
        }

        public override int Count
        {
            get { return momenten.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

    }
}