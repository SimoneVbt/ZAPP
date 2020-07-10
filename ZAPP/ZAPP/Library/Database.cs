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
    public class Database
    {
        private readonly Context context;
        private readonly string dbpath;
        private readonly string connectionString;
        private readonly string url = "http://192.168.0.105/zapp/zapp_api/public/index.php/api/zorgmoment/get/1";

        public Database(Context context)
        {
            this.context = context;
            Resources res = context.Resources;
            string app_name = res.GetString(Resource.String.app_name);
            string app_version = res.GetString(Resource.String.app_version);
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string dbname = "db_" + app_name + "_" + app_version + ".sqlite";

            dbpath = Path.Combine(documentsPath, dbname);
            connectionString = $"Data Source={dbpath};Version=3;";

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
                    ClientRecord clientRecord = new ClientRecord(result);
                    InsertClientData(clientRecord);
                    ZorgmomentRecord momentRecord = new ZorgmomentRecord(result);
                    InsertZorgmomenten(momentRecord);
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

        //----------------ZORGMOMENT TABLE----------------

        public void InsertZorgmomenten(ZorgmomentRecord record)
        {
            var check = CheckRecord(record.id.ToString(), "zorgmoment");

            if (check == false)
            {
                var conn = new SqliteConnection(connectionString);

                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "insert into zorgmoment (id, client_id, datum_tijd, opmerkingen, aanwezigheid_begin, aanwezigheid_eind) " +
                    $"values ({record.id}, {record.client_id}, '{record.datum_tijd}', '{record.opmerkingen}', '{record.aanwezigheid_begin}', '{record.aanwezigheid_eind}')";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                Console.WriteLine("Record toegevoegd: moment " + record.id + " voor cliënt " + record.client_id);

                conn.Close();
            }
        }

        public ZorgmomentRecord GetZorgmomentRecordById(string id)
        {
            var conn = new SqliteConnection(connectionString);

            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT * FROM zorgmoment WHERE id = {id}";
            cmd.CommandType = CommandType.Text;
            SqliteDataReader reader = cmd.ExecuteReader();

            ZorgmomentRecord record = new ZorgmomentRecord(reader);

            conn.Close();

            return record;
        }

        public ArrayList GetAllZorgmomenten()
        {
            var conn = new SqliteConnection(connectionString);

            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM zorgmoment";
            cmd.CommandType = CommandType.Text;
            SqliteDataReader reader = cmd.ExecuteReader();

            ArrayList momentRecords = new ArrayList();
            while (reader.Read())
            {
                ZorgmomentRecord row = new ZorgmomentRecord(reader);
                momentRecords.Add(row);
            }

            conn.Close();

            return momentRecords;
        }

        //----------------CLIENT TABLE----------------

        public void InsertClientData(ClientRecord record)
        {
            var check = CheckRecord(record.id.ToString(), "client");

            if (check == false)
            {
                var conn = new SqliteConnection(connectionString);

                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "insert into client (id, achternaam, voornaam, adres, postcode, woonplaats, telefoonnummer) " +
                    $"values ({record.id}, '{record.achternaam}', '{record.voornaam}', '{record.adres}', '{record.postcode}', '{record.woonplaats}', '{record.telefoonnummer}')";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                Console.WriteLine("Record toegevoegd: " + record.voornaam + " " + record.achternaam);

                conn.Close();
            }
            
        }

        public ClientRecord GetClientRecordById(string id)
        {
            var conn = new SqliteConnection(connectionString);

            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT * FROM client WHERE id = {id}";
            cmd.CommandType = CommandType.Text;
            SqliteDataReader reader = cmd.ExecuteReader();

            ClientRecord record = new ClientRecord(reader);

            conn.Close();

            return record;
        }

        public ArrayList GetAllClientData()
        {
            var conn = new SqliteConnection(connectionString);

            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM client";
            cmd.CommandType = CommandType.Text;
            SqliteDataReader reader = cmd.ExecuteReader();

            ArrayList clientRecords = new ArrayList();
            while (reader.Read())
            {
                ClientRecord row = new ClientRecord(reader);
                clientRecords.Add(row);
            }

            conn.Close();

            return clientRecords;
        }

    }
}