using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Json;
using System.Collections;
using System.Data;

using Mono.Data.Sqlite;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ZAPP
{
    class Database
    {
        private readonly Context context;
        private readonly string dbpath;
        private readonly string connectionString;

        public Database(Context context)
        {
            this.context = context;
            Resources res = context.Resources;
            string app_name = res.GetString(Resource.String.app_name);
            string app_version = res.GetString(Resource.String.app_version);
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string dbname = "db_" + app_name + "_" + app_version + ".sqlite";

            dbpath = Path.Combine(documentsPath, dbname);
            connectionString = String.Format("Data Source={0};Version=3;", dbpath);

            CreateDatabase();
        }

        public void CreateDatabase()
        {
            Resources res = context.Resources;
            string createTableGebruiker = res.GetString(Resource.String.create_table_gebruiker);
            string createTableZorgmoment = res.GetString(Resource.String.create_table_zorgmoment);
            string createTableClient = res.GetString(Resource.String.create_table_client);
            string createTableTaak = res.GetString(Resource.String.create_table_taak);

            string[] commands = { createTableGebruiker, createTableZorgmoment, createTableClient, createTableTaak };

            if (!File.Exists(dbpath))
            {
                SqliteConnection.CreateFile(dbpath);
                var conn = new SqliteConnection(connectionString);

                conn.Open();

                for (int i = 0; i < commands.Length; i++)
                {
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = commands[i];
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        private readonly string url = "http://192.168.1.244/zapp_api/public/index.php/api/zorgmoment/get/1";

        public void DownloadData()
        {
            var webClient = new WebClient()
            {
                Encoding = Encoding.UTF8
            };

            try
            {
                byte[] myDataBuffer = webClient.DownloadData(url);

                string download = Encoding.ASCII.GetString(myDataBuffer);
                JsonValue value = JsonValue.Parse(download);
                foreach(JsonObject result in value)
                {
                    Console.WriteLine("opmerkingen: " + result["moment"]["opmerkingen"]);
                }

            }
            catch (WebException e)
            {
                Console.WriteLine("exception: " + e.Message);
            }
        }
    }
}