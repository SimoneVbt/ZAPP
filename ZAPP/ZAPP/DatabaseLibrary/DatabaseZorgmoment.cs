using System;
using System.IO;
using System.Data;
using System.Net.Http;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using Android.Content;
using Android.Content.Res;

namespace ZAPP
{
    public class DatabaseZorgmoment
    {
        private readonly Context context;
        private readonly string connectionString;
        private readonly string url = "http://192.168.0.109/zapp/zapp_api/public/index.php/api/zorgmoment/update/";
        //private readonly string url = "http://192.168.1.244/zapp/zapp_api/public/index.php/api/zorgmoment/update/";

        public DatabaseZorgmoment(Context context)
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

        public void UpdateZorgmomenten()
        {
            DeleteOldRecords();
            RemoveNieuwOnLogin();
        }

        public void DeleteOldRecords()
        {
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            string today = DateTime.Today.ToString("yyyy-MM-dd");

            var cmd = conn.CreateCommand();
            cmd.CommandText = $"DELETE FROM zorgmoment WHERE datum_tijd < '{today}'";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void RemoveNieuwOnLogin()
        {
            ArrayList zorgmomenten = GetAllZorgmomenten();

            var conn = new SqliteConnection(connectionString);
            conn.Open();

            foreach (ZorgmomentRecord moment in zorgmomenten)
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = $"UPDATE zorgmoment SET nieuw = 0 WHERE id = {moment.id}";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }

        public async void UpdateAanwezigheid(string moment_id, string begin_of_eind, string tijdstip)
        {
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = $"UPDATE zorgmoment SET aanwezigheid_{begin_of_eind} = '{tijdstip}' WHERE id = {moment_id}";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();

            HttpClient client = new HttpClient();
            HttpContent content = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("aanwezigheid_" + begin_of_eind, tijdstip)
            });
            string momentUrl = url + moment_id;
            await client.PostAsync(momentUrl, content);
        }

        public void InsertZorgmomenten(ZorgmomentRecord record)
        {
            Database db = new Database(context);
            var check = db.CheckRecord(record.id.ToString(), "zorgmoment");

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
                cmd.CommandText = "INSERT INTO zorgmoment (id, client_id, datum_tijd, opmerkingen, aanwezigheid_begin, aanwezigheid_eind, nieuw) " +
                    $"VALUES ({record.id}, {record.client_id}, '{record.datum_tijd}', '{record.opmerkingen}', '{record.aanwezigheid_begin}', '{record.aanwezigheid_eind}', 1)";
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
            cmd.CommandText = "SELECT * FROM zorgmoment ORDER BY datum_tijd ASC";
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

        public ZorgmomentRecord GetZorgmomentById(string id)
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
    }
}