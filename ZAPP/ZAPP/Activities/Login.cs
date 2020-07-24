using System;
using System.Collections.Generic;
using System.Net.Http;
using Android.App;
using Android.OS;
using Android.Widget;

namespace ZAPP
{
    [Activity(Label = "Login", NoHistory = true)]
    public class Login : Activity
    {
        private readonly string url = "http://192.168.0.109/zapp/zapp_api/public/index.php/api/gebruiker/login";
        //private readonly string url = "http://192.168.1.244/zapp/zapp_api/public/index.php/api/gebruiker/login";
        Button login;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Login);
            login = FindViewById<Button>(Resource.Id.LoginSubmit);
            login.Click += new EventHandler(PostData);
        }

        public async void PostData(object sender, object e)
        {
            string gebruikersnaam = FindViewById<EditText>(Resource.Id.Username).Text;
            string wachtwoord = FindViewById<EditText>(Resource.Id.Password).Text;

            HttpClient client = new HttpClient();
            HttpContent content = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("gebruikersnaam", gebruikersnaam),
                new KeyValuePair<string, string>("wachtwoord", wachtwoord),
            });
            HttpResponseMessage response = await client.PostAsync(url, content);
            string result = await response.Content.ReadAsStringAsync();

            if (result != "-1" && result != null)
            {
                DatabaseLogin dbl = new DatabaseLogin(this);
                dbl.Login(result);
                DatabaseZorgmoment dbz = new DatabaseZorgmoment(this);
                dbz.UpdateZorgmomenten();
                Database db = new Database(this);
                db.DownloadData(result);
                StartActivity(typeof(Home));
            }
            else
            {
                FindViewById<TextView>(Resource.Id.FlashText).Text = "Foutieve gebruikersnaam en/of wachtwoord";
                FindViewById<EditText>(Resource.Id.Username).Text = "";
                FindViewById<EditText>(Resource.Id.Password).Text = "";
            }
        }
    }
}