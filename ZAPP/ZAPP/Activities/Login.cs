using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ZAPP
{
    [Activity(Label = "Login", NoHistory = true)]
    public class Login : Activity
    {
        //private readonly string url = "http://192.168.0.105/zapp/zapp_api/public/index.php/api/gebruiker/login";
        private readonly string url = "http://192.168.1.244/zapp/zapp_api/public/index.php/api/gebruiker/login";
        Button btn;
        TextView text;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Login);
            btn = FindViewById<Button>(Resource.Id.LoginSubmit);
            btn.Click += new EventHandler(PostData);
        }

        public async void PostData(object sender, object e)
        {
            string gebruikersnaam = FindViewById<EditText>(Resource.Id.Username).Text;
            string wachtwoord = FindViewById<EditText>(Resource.Id.Password).Text;

            IEnumerable<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("gebruikersnaam", gebruikersnaam),
                new KeyValuePair<string, string>("wachtwoord", wachtwoord),
            };

            HttpClient client = new HttpClient();
            HttpContent content = new FormUrlEncodedContent(data);
            HttpResponseMessage response = await client.PostAsync(url, content);
            string result = await response.Content.ReadAsStringAsync();

            if (result != "-1" && result != null)
            {
                Database db = new Database(this);
                Console.WriteLine(result.ToString());
                db.Login(result.ToString());
                db.DownloadData(result.ToString());
                
                var intent = new Intent(this, typeof(Home));
                intent.PutExtra("id", result.ToString());
                StartActivityForResult(intent, 0);
            }
            else
            {
                StartActivity(typeof(Login));
                // klopt nog niet, verschijnt op het scherm vlak voordat scherm wordt vernieuwd
                text = FindViewById<TextView>(Resource.Id.FlashText);
                text.Text = "Foutieve gebruikersnaam en/of wachtwoord";
            }
        }
    }
}