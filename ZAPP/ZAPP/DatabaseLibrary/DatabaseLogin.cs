using System;
using System.IO;
using System.Data;
using Mono.Data.Sqlite;
using Android.Content;
using Android.Content.Res;

namespace ZAPP
{
    public class DatabaseLogin
    {
        private readonly string connectionString;

        public DatabaseLogin(Context context)
        {
            Resources res = context.Resources;
            string app_name = res.GetString(Resource.String.app_name);
            string app_version = res.GetString(Resource.String.app_version);
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string dbname = "db_" + app_name + "_" + app_version + ".sqlite";
            string dbpath = Path.Combine(documentsPath, dbname);
            connectionString = $"Data Source={dbpath};Version=3;";
        }

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

        public bool CheckLogin()
        {
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM gebruiker";
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

        public GebruikerRecord GetGebruiker()
        {
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM gebruiker";
            cmd.CommandType = CommandType.Text;

            SqliteDataReader reader = cmd.ExecuteReader();
            GebruikerRecord record = new GebruikerRecord(reader);

            conn.Close();
            return record;
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
    }
}