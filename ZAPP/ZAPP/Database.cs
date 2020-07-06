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

        public Database (Context context)
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
            string createTableClient = res.GetString(Resource.String.create_table_client);
            string createTableGebruiker = res.GetString(Resource.String.create_table_gebruiker);
            string createTableTaak = res.GetString(Resource.String.create_table_taak);
            string createTableZorgmoment = res.GetString(Resource.String.create_table_zorgmoment);

            string[] commands = { createTableClient, createTableGebruiker, createTableTaak, createTableZorgmoment };

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


        private readonly string url_client = "https://localhost:8000/api/clients.json";
        private readonly string url_gebruiker = "https://localhost:8000/api/gebruikers.json";
        private readonly string url_taak = "https://localhost:8000/api/taaks.json";
        private readonly string url_zorgmoment = "https://localhost:8000/api/zorgmoments.json";

        /*
        public void DownloadData()
        {
            WebClient webClient = new WebClient
            {
                Encoding = Encoding.UTF8
            };

            try
            {
                byte[] myDataBuffer = webClient.DownloadData(url_client);
                string download = Encoding.ASCII.GetString(myDataBuffer);
                JsonValue value = JsonValue.Parse(download);

                var conn = new SqliteConnection(connectionString);
                conn.Open();

                foreach (JsonObject result in value)
                {
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = String.Format("insert into client (id, achternaam, voornaam, adres, postcode, woonplaats, telefoonnummer) values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", 
                                                        result.id, result.achternaam, result.voornaam, result.adres, result.postcode, result.woonplaats, result.telefoonnummer);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
            catch (WebException)
            {
                string answer = "Cliëntgegevens laden gelukt";
            }
            
        }
        */


    }
}