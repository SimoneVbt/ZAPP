using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace ZAPP
{
    [Activity(Label = "DetailListViewAdapter")]
    class DetailListViewAdapter : BaseAdapter<ListTaakRecord>
    {
        readonly List<ListTaakRecord> taken;
        readonly Activity context;

        public DetailListViewAdapter(Activity context, List<ListTaakRecord> taken)
            : base()
        {
            this.context = context;
            this.taken = taken;
        }


        public override ListTaakRecord this[int position]
        {
            get { return taken[position]; }
        }

        public override int Count
        {
            get { return taken.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var taak = taken[position];
            View view = convertView;

            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.ListRow, null);
            }
            view.FindViewById<TextView>(Resource.Id.TextLeftBig).Text = $"{taak.stap} - {taak.omschrijving}";
            view.FindViewById<TextView>(Resource.Id.TextRightBig).Text = "o";

            return view;
        }
    }
}