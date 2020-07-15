using System;
using System.Json;
using Mono.Data.Sqlite;

namespace ZAPP
{
    public class GebruikerRecord
    {
        public int id;

        public GebruikerRecord(JsonValue record)
        {
            id = record["id"];
        }

        public GebruikerRecord(SqliteDataReader record)
        {
            id = (int)(Int64) record["id"];
        }
    }
}