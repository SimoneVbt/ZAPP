using Android.App;
using Android.OS;

namespace ZAPP
{

    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        private GebruikerRecord gebruiker;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Database db = new Database(this);
            db.CreateDatabase();

            DatabaseLogin dbl = new DatabaseLogin(this);
            DatabaseZorgmoment dbz = new DatabaseZorgmoment(this);

            if (dbl.CheckLogin())
            {
                gebruiker = dbl.GetGebruiker();
                dbz.UpdateZorgmomenten();
                db.DownloadData(gebruiker.id.ToString());
                StartActivity(typeof(Home));
            }
            else
            {
                StartActivity(typeof(Login));
            }
        }
    }
}