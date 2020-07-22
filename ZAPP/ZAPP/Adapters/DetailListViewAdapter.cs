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
                view = context.LayoutInflater.Inflate(Resource.Layout.ListRow, parent, false);
            }

            TextView description = view.FindViewById<TextView>(Resource.Id.TextLeftBig);
            description.Text = $"{taak.stap} - {taak.omschrijving}";

            TextView completion = view.FindViewById<TextView>(Resource.Id.TextRightBig);
            completion.TextSize = 30;

            switch (taak.voltooid)
            {
                case "0":
                    completion.Text = "O";
                    break;
                case "1":
                    completion.Text = "V";
                    completion.SetTextColor(Color.ParseColor("#00a000"));
                    description.SetTextColor(Color.ParseColor("#00a000"));
                    break;
            }

            DatabaseTaak dbt = new DatabaseTaak(context);
            LinearLayout row = view.FindViewById<LinearLayout>(Resource.Id.ListViewRow);
            row.Click += (object sender, EventArgs e) =>
            {
                switch (completion.Text)
                {
                    case "V":
                        completion.Text = "O";
                        completion.SetTextColor(Color.ParseColor("#3D3D3D"));
                        description.SetTextColor(Color.ParseColor("#3D3D3D"));
                        dbt.UpdateTaak(taak.id, "0");
                        break;
                    case "O":
                        completion.Text = "V";
                        completion.SetTextColor(Color.ParseColor("#00a000"));
                        description.SetTextColor(Color.ParseColor("#00a000"));
                        dbt.UpdateTaak(taak.id, "1");
                        break;
                }
            };

            view.FindViewById<TextView>(Resource.Id.TextLeftSmall).Visibility = ViewStates.Gone;
            view.FindViewById<TextView>(Resource.Id.TextRightSmall).Visibility = ViewStates.Gone;

            return view;
        }
    }
}