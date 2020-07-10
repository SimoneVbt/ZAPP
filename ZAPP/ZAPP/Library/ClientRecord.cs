using System.Json;
using System.Collections;
using Mono.Data.Sqlite;
using System;

namespace ZAPP
{
    public class ClientRecord
    {
        public int id;
        public string achternaam;
        public string voornaam;
        public string adres;
        public string postcode;
        public string woonplaats;
        public string telefoonnummer;

        public ClientRecord(JsonValue record)
        {
            id = record["client"]["id"];
            achternaam = record["client"]["achternaam"];
            voornaam = record["client"]["voornaam"];
            adres = record["client"]["adres"];
            postcode = record["client"]["postcode"];
            woonplaats = record["client"]["woonplaats"];
            telefoonnummer = record["client"]["telefoonnummer"];
        }

        
        public ClientRecord(SqliteDataReader record)
        {
            id = (int)(Int64) record["id"];
            achternaam = (string) record["achternaam"];
            voornaam = (string) record["voornaam"];
            adres = (string) record["adres"];
            postcode = (string) record["postcode"];
            woonplaats = (string) record["woonplaats"];
            telefoonnummer = (string)record["telefoonnummer"];
        }
        
    }
}