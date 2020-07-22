using System;
using System.Text;
using System.IO;
using System.Net;
using System.Json;
using System.Collections;
using System.Data;
using Mono.Data.Sqlite;
using Android.Content;
using Android.Content.Res;

namespace ZAPP
{
    public class DatabaseClient
    {
        private readonly Context context;
        private readonly string connectionString;

        public DatabaseClient (Context context)
        {
            this.context = context;
            Resources res = context.Resources;
            string app_name = res.GetString(Resource.String.app_name);
            string app_version = res.GetString(Resource.String.app_version);
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string dbname = "db_" + app_name + "_" + app_version + ".sqlite";
            string dbpath = Path.Combine(documentsPath, dbname);
            connectionString = $"Data Source={dbpath};Version=3;";
        }


        public void InsertClientData(ClientRecord record)
        {
            Database db = new Database(context);
            bool check = db.CheckRecord(record.id.ToString(), "client");

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