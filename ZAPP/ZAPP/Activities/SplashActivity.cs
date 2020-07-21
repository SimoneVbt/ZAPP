using Android.App;
using Android.OS;

namespace ZAPP
{

    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        private GebruikerRecord record;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Database db = new Database(this);
            db.CreateDatabase();

            if (db.CheckLogin())
            {
                record = db.GetGebruiker();
                db.DownloadData(record.id.ToString());
                StartActivity(typeof(Home));
            }
            else
            {
                StartActivity(typeof(Login));
            }
        }
    }
}