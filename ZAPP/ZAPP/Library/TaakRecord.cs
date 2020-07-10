using System.Json;
using System.Collections;
using Mono.Data.Sqlite;


namespace ZAPP
{
    public class TaakRecord
    {
        public int id;
        public int zorgmoment_id;
        public int stap;
        public string omschrijving;
        public bool voltooid;

        public TaakRecord(JsonValue record)
        {
            id = record["id"];
            zorgmoment_id = record["zorgmoment_id"];
            stap = record["stap"];
            omschrijving = record["omschrijving"];
            voltooid = record["voltooid"];
        }
    }
}