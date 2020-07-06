using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mono.Data.Sqlite;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Json;

namespace ZAPP
{
    public class TaakRecord
    {
        public int id;
        public string zorgmoment;
        public string omschrijving;
        public bool voltooid;
        public int stap;

        public TaakRecord (JsonValue record)
        {
            id = record["id"];
            zorgmoment = record["zorgmoment"];
            omschrijving = record["omschrijving"];
            voltooid = record["voltooid"];
            stap = record["stap"];
        }

        public TaakRecord (SqliteDataReader record)
        {
            id = (int)(Int64)record["id"];
            zorgmoment = (string)record["zorgmoment"];
            omschrijving = (string)record["omschrijving"];
            voltooid = (bool)record["voltooid"];
            stap = (int)(Int64)record["stap"];
        }
    }
}