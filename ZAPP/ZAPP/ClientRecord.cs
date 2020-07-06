using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;

using Mono.Data.Sqlite;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

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
        public string[] zorgmomenten;

        public ClientRecord (JsonValue record)
        {
            id = record["id"];
            achternaam = record["achternaam"];
            voornaam = record["voornaam"];
            adres = record["achternaam"];
            postcode = record["postcode"];
            woonplaats = record["woonplaats"];
            telefoonnummer = record["telefoonnummer"];
        }
        
        public ClientRecord (SqliteDataReader record)
        {
            id = (int)(Int64)record["id"];
            achternaam = (string)record["achternaam"];
            voornaam = (string)record["voornaam"];
            adres = (string)record["adres"];
            postcode = (string)record["postcode"];
            woonplaats = (string)record["woonplaats"];
            telefoonnummer = (string)record["telefoonnummer"];
        }
    }
}