using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using System;

namespace ZAPP
{
    [Activity(Label = "DetailListViewAdapter")]
    class DetailListViewAdapter : BaseAdapter<ListTaakRecord>
    {
        readonly List<ListTaakRecord> taken;
        readonly Activity context;
        TextView completion;

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

            TextView description = view.FindViewById<TextView>(Resource.Id.TextLeftBig);
            description.Text = $"{taak.stap} - {taak.omschrijving}";

            completion = view.FindViewById<TextView>(Resource.Id.TextRightBig);
            completion.Text = "O";
            completion.TextSize = 26;
            completion.SetTextColor(Color.ParseColor("#aa00ca"));

            completion.Click += CompletionChange;

            view.FindViewById<TextView>(Resource.Id.TextLeftSmall).Visibility = ViewStates.Gone;
            view.FindViewById<TextView>(Resource.Id.TextRightSmall).Visibility = ViewStates.Gone;

            return view;
        }

        public void CompletionChange(object sender, EventArgs e)
        {
            if (completion.Text == "V")
            {
                completion.Text = "O";
                completion.SetTextColor(Color.ParseColor("#aa00ca"));
            }
            else
            {
                completion.Text = "V";
                completion.SetTextColor(Color.ParseColor("#00e000"));
            }
            
        }
    }
}