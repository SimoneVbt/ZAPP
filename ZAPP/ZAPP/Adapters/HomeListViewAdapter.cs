using System;
using System.Collections.Generic;
using System.Globalization;
using Android.App;
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

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var moment = momenten[position];
            Database db = new Database(context);
            var client = db.GetClientById(moment.client_id);

            View view = convertView;

            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.ListRow, null);
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

            return view;
        }
         
    }
}