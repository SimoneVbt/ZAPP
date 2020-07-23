using System;
using System.Text;
using System.IO;
using System.Net;
using System.Json;
using System.Data;
using Mono.Data.Sqlite;
using Android.Content;
using Android.Content.Res;

namespace ZAPP
{
    public class Database
    {
        private readonly Context context;
        private readonly string dbpath;
        private readonly string connectionString;
        //private readonly string url = "http://192.168.0.109/zapp/zapp_api/public/index.php/api/zorgmoment/get/";
        private readonly string url = "http://192.168.1.244/zapp/zapp_api/public/index.php/api/zorgmoment/get/";

        public Database(Context context)
        {
            this.context = context;
            Resources res = context.Resources;
            string app_name = res.GetString(Resource.String.app_name);
            string app_version = res.GetString(Resource.String.app_version);
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string dbname = "db_" + app_name + "_" + app_version + ".sqlite";

            dbpath = Path.Combine(documentsPath, dbname);
            connectionString = $"Data Source={dbpath};Version=3;";
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

        public void DownloadData(string id)
        {
            var webClient = new WebClient()
            {
                Encoding = Encoding.UTF8
            };

            string personalisedUrl = url + id;

            try
            {
                byte[] myDataBuffer = webClient.DownloadData(personalisedUrl);

                string download = Encoding.ASCII.GetString(myDataBuffer);
                JsonValue value = JsonValue.Parse(download);

                foreach(JsonObject result in value)
                {
                    ClientRecord clientRecord = new ClientRecord(result);
                    DatabaseClient dbc = new DatabaseClient(context);
                    dbc.InsertClientData(clientRecord);

                    ZorgmomentRecord momentRecord = new ZorgmomentRecord(result);
                    DatabaseZorgmoment dbz = new DatabaseZorgmoment(context);
                    dbz.InsertZorgmomenten(momentRecord);

                    DatabaseTaak dbt = new DatabaseTaak(context);
                    foreach (JsonObject taak in result["taken"])
                    {
                        TaakRecord taakRecord = new TaakRecord(taak);
                        dbt.InsertTaken(taakRecord);
                    }
                }

            }
            catch (WebException e)
            {
                Console.WriteLine("exception: " + e.Message);
            }
        }

        public bool CheckRecord (string id, string tablename)
        {
            var conn = new SqliteConnection(connectionString);

            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT * FROM {tablename} WHERE id = {id}";
            cmd.CommandType = CommandType.Text;
            SqliteDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                conn.Close();
                return true;
            }

            conn.Close();
            return false;
        }
    }
}