using System;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace ZAPP
{

    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Database db = new Database(this);
            db.CreateDatabase();

            int user_id = db.CheckLogin();
            //user_id = 0, werken via GebruikerRecord want dit werkt niet
            if (user_id > 0)
            {
                StartActivity(typeof(Home));
                db.DownloadData(user_id.ToString());
            }
            else
            {
                StartActivity(typeof(Login));
            }
            

        }
    }
}