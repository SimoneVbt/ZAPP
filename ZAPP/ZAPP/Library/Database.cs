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
        //private readonly string url = "http://192.168.0.105/zapp/zapp_api/public/index.php/api/zorgmoment/get/";
        private readonly string url = "http://192.168.1.244/zapp/zapp_api/public/index.php/api/zorgmoment/get/";

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
                    InsertClientData(clientRecord);
                    ZorgmomentRecord momentRecord = new ZorgmomentRecord(result);
                    InsertZorgmomenten(momentRecord);
                    foreach (JsonObject taak in result["taken"])
                    {
                        TaakRecord taakRecord = new TaakRecord(taak);
                        InsertTaken(taakRecord);
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
        //----------------LOGIN----------------

        public void Login(string id)
        {
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = $"INSERT INTO gebruiker (id) VALUES ('{id}')";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public int CheckLogin()
        {
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM gebruiker";
            cmd.CommandType = CommandType.Text;
            int result = cmd.ExecuteNonQuery();

            return result;
        }

        public void Logout()
        {
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM gebruiker";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();
        }


        //----------------TAAK TABLE----------------

        public void InsertTaken(TaakRecord record)
        {
            var check = CheckRecord(record.id.ToString(), "taak");

            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            if (check)
            {
                cmd.CommandText = "UPDATE taak " +
                    $"SET zorgmoment_id = {record.zorgmoment_id}, stap = {record.stap}, omschrijving = '{record.omschrijving}', stap = {record.stap} " +
                    $"WHERE id = {record.id}";
            }
            else
            {
                cmd.CommandText = "INSERT INTO taak (id, zorgmoment_id, stap, omschrijving, voltooid) " +
                    $"VALUES ({record.id}, {record.zorgmoment_id}, {record.stap}, '{record.omschrijving}', {record.voltooid})";
            }
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public ArrayList GetTakenByZorgmoment(string moment_id)
        {
            var conn = new SqliteConnection(connectionString);

            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT * FROM taak WHERE zorgmoment_id = {moment_id}";
            cmd.CommandType = CommandType.Text;
            SqliteDataReader reader = cmd.ExecuteReader();

            ArrayList taakRecords = new ArrayList();
            while (reader.Read())
            {
                TaakRecord row = new TaakRecord(reader);
                taakRecords.Add(row);
            }

            conn.Close();

            return taakRecords;
        }

        //----------------ZORGMOMENT TABLE----------------

        public void InsertZorgmomenten(ZorgmomentRecord record)
        {
            var check = CheckRecord(record.id.ToString(), "zorgmoment");

            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            if (check)
            {
                cmd.CommandText = "UPDATE zorgmoment " +
                    $"SET client_id = {record.client_id}, datum_tijd = '{record.datum_tijd}', opmerkingen = '{record.opmerkingen}', aanwezigheid_begin = '{record.aanwezigheid_begin}', aanwezigheid_eind = '{record.aanwezigheid_eind}' " +
                    $"WHERE id = {record.id}";
            } 
            else
            {
                cmd.CommandText = "INSERT INTO zorgmoment (id, client_id, datum_tijd, opmerkingen, aanwezigheid_begin, aanwezigheid_eind) " +
                    $"VALUES ({record.id}, {record.client_id}, '{record.datum_tijd}', '{record.opmerkingen}', '{record.aanwezigheid_begin}', '{record.aanwezigheid_eind}')";
            }
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();
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
            bool check = CheckRecord(record.id.ToString(), "client");

            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();

            if (check)
            {
                cmd.CommandText = $"UPDATE client " +
                    $"SET achternaam = '{record.achternaam}', voornaam = '{record.voornaam}', adres = '{record.adres}', postcode = '{record.postcode}', woonplaats = '{record.woonplaats}', telefoonnummer = '{record.telefoonnummer}' " +
                    $"WHERE id = {record.id}";
            }
            else
            {
                cmd.CommandText = "INSERT INTO client (id, achternaam, voornaam, adres, postcode, woonplaats, telefoonnummer) " +
                    $"VALUES ({record.id}, '{record.achternaam}', '{record.voornaam}', '{record.adres}', '{record.postcode}', '{record.woonplaats}', '{record.telefoonnummer}')";
            }

            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();

        }

        public ClientRecord GetClientById(string id)
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

    }
}