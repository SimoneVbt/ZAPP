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
using Org.Apache.Http.Util;
using Org.Json;

namespace ZAPP
{
    [Activity(Label = "Login", NoHistory = true)]
    public class Login : Activity
    {
        private readonly string url = "http://192.168.0.105/zapp/zapp_api/public/index.php/api/gebruiker/login";
        // private readonly string url = "http://192.168.1.244/zapp/zapp_api/public/index.php/api/gebruiker/login";
        Button btn;

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

            //werkt niet: result is null
            string result = await response.Content.ReadAsStringAsync();

            if (result != "-1" && result != null)
            {
                StartActivity(typeof(Home));
            }
            else
            {

            }
        }
    }
}