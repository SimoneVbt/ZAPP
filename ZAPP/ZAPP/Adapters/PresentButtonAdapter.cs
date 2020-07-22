using Android.App;
using Android.Views;
using Android.Widget;

namespace ZAPP
{
    public class PresentButtonAdapter : BaseAdapter
    {

        readonly Activity context;

        public PresentButtonAdapter(Activity context)
        {
            this.context = context;
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

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.PresentButton, parent, false);
            }

            return view;
        }

    }
}