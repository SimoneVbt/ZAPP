using System;
using System.IO;
using System.Collections;
using System.Data;
using Mono.Data.Sqlite;
using Android.Content;
using Android.Content.Res;

namespace ZAPP
{
    public class DatabaseTaak
    {
        private readonly Context context;
        private readonly string connectionString;

        public DatabaseTaak (Context context)
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

        public void UpdateTaak(string taak_id, string voltooid_bool)
        {
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = $"UPDATE taak SET voltooid = {voltooid_bool} WHERE id = {taak_id}";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void InsertTaken(TaakRecord record)
        {
            Database db = new Database(context);
            var check = db.CheckRecord(record.id.ToString(), "taak");

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
    }
}