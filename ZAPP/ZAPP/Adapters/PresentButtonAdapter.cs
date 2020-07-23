using System;
using Android.App;
using Android.Content.Res;
using Android.Graphics;
using Android.Views;
using Android.Widget;

namespace ZAPP
{
    public class PresentButtonAdapter : BaseAdapter
    {

        readonly Activity context;
        readonly ZorgmomentRecord zorgmoment;
        readonly Resources res;
        Button aanwezigheid;

        public PresentButtonAdapter(Activity context)
        {
            this.context = context;
            zorgmoment = Global.zorgmoment;
            res = context.Resources;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.PresentButton, parent, false);
            }

            aanwezigheid = view.FindViewById<Button>(Resource.Id.AanwezigButton);
            aanwezigheid.Click += UpdatePresence;

            if (zorgmoment.aanwezigheid_eind != "")
            {
                aanwezigheid.SetBackgroundColor(Color.ParseColor("#aa00ca"));
                aanwezigheid.SetTextColor(Color.ParseColor("#FFFFFF"));
                aanwezigheid.Text = res.GetString(Resource.String.aanwezigheid_uit);
            }
            else if (zorgmoment.aanwezigheid_begin != "")
            {
                aanwezigheid.SetBackgroundColor(Color.ParseColor("#00c000"));
                aanwezigheid.Text = res.GetString(Resource.String.aanwezigheid_aan);
            }

            return view;
        }

        public void UpdatePresence(object sender, EventArgs e)
        {
            DatabaseZorgmoment dbz = new DatabaseZorgmoment(context);

            DateTime utc = DateTime.UtcNow;
            TimeZoneInfo west = TimeZoneInfo.FindSystemTimeZoneById("Europe/Amsterdam");
            DateTime tijdstip = TimeZoneInfo.ConvertTimeFromUtc(utc, west);

            if (zorgmoment.aanwezigheid_begin != "" && zorgmoment.aanwezigheid_eind == "")
            {
                dbz.UpdateAanwezigheid(zorgmoment.id.ToString(), "eind", tijdstip.ToString());
                ZorgmomentRecord moment_record = dbz.GetZorgmomentById(zorgmoment.id.ToString());
                Global.zorgmoment = moment_record;

                aanwezigheid.SetBackgroundColor(Color.ParseColor("#aa00ca"));
                aanwezigheid.SetTextColor(Color.ParseColor("#FFFFFF"));
                aanwezigheid.Text = res.GetString(Resource.String.aanwezigheid_uit);
            }
            else if (zorgmoment.aanwezigheid_begin == "")
            {
                dbz.UpdateAanwezigheid(zorgmoment.id.ToString(), "begin", tijdstip.ToString());
                ZorgmomentRecord moment_record = dbz.GetZorgmomentById(zorgmoment.id.ToString());
                Global.zorgmoment = moment_record;
                aanwezigheid.SetBackgroundColor(Color.ParseColor("#00c000"));
                aanwezigheid.Text = res.GetString(Resource.String.aanwezigheid_aan);
            }
        }

        public override int Count
        {
            get { return 1; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

    }
}