﻿using System;
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
            
            if (db.CheckLogin())
            {
                StartActivity(typeof(Home));
                db.CreateDatabase();
                db.DownloadData();
            }
            else
            {
                StartActivity(typeof(Login));
            }
            

        }
    }
}