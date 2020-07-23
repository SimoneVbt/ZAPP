using System;
using System.Json;
using Mono.Data.Sqlite;

namespace ZAPP
{
    public class TaakRecord
    {
        public int id;
        public int zorgmoment_id;
        public int stap;
        public string omschrijving;
        public int voltooid;

        public TaakRecord(JsonValue record)
        {
            id = record["id"];
            zorgmoment_id = record["zorgmoment_id"];
            stap = record["stap"];
            omschrijving = record["omschrijving"];
            voltooid = record["voltooid"];
        }

        public TaakRecord(SqliteDataReader record)
        {
            id = (int)(Int64) record["id"];
            zorgmoment_id = (int)(Int64) record["zorgmoment_id"];
            stap = (int)(Int64) record["stap"];
            omschrijving = (string) record["omschrijving"];
            voltooid = (int)(Int32) record["voltooid"];
        }
    }
}