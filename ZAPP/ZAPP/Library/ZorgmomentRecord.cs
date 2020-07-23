using System;
using System.Json;
using Mono.Data.Sqlite;

namespace ZAPP
{
    public class ZorgmomentRecord
    {
        public int id;
        public int client_id;
        public string datum_tijd;
        public string opmerkingen;
        public string aanwezigheid_begin;
        public string aanwezigheid_eind;
        public int nieuw;

        public ZorgmomentRecord(JsonValue record)
        {
            id = record["moment"]["id"];
            client_id = record["client"]["id"];
            datum_tijd = record["moment"]["datum_tijd"];
            opmerkingen = record["moment"]["opmerkingen"];
            aanwezigheid_begin = record["moment"]["aanwezigheid_begin"];
            aanwezigheid_eind = record["moment"]["aanwezigheid_eind"];
        }

        public ZorgmomentRecord(SqliteDataReader record)
        {
            id = (int)(Int64) record["id"];
            client_id = (int)(Int64) record["client_id"];
            datum_tijd = (string) record["datum_tijd"];
            opmerkingen = (string) record["opmerkingen"];
            aanwezigheid_begin = (string) record["aanwezigheid_begin"];
            aanwezigheid_eind = (string) record["aanwezigheid_eind"];
            nieuw = (int)(Int32) record["nieuw"];
        }
    }

    
}