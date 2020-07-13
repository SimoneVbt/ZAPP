using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ZAPP
{
    [Activity(Label = "HomeListViewAdapter")]
    public class HomeListViewAdapter : BaseAdapter<ListZorgmomentRecord>
    {

        readonly List<ListZorgmomentRecord> items;
        readonly Activity context;

        public HomeListViewAdapter(Activity context, List<ListZorgmomentRecord> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }


        public override ListZorgmomentRecord this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;

            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.ListRow, null);
            }
            view.FindViewById<TextView>(Resource.Id.Text1).Text = "id: " + item.id;
            view.FindViewById<TextView>(Resource.Id.Text2).Text = "client_id: " + item.client_id; //naam
            view.FindViewById<TextView>(Resource.Id.Text3).Text = "datum_tijd: " + item.datum_tijd; //goede format

            return view;
        }

    }
}