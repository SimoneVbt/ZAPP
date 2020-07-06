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
using Java.Sql;

namespace ZAPP
{
    public class ZorgmomentRecord
    {
        public int id;
        public string client_id;
        public string datumTijd;
        public string opmerkingen;

        public ZorgmomentRecord (JsonValue record)
        {
            id = record["id"];
            client_id = record["client"];
            datumTijd = record["datumTijd"];
            opmerkingen = record["opmerkingen"];
        }

        public ZorgmomentRecord (SqliteDataReader record)
        {
            id = (int)(Int64)record["id"];
            client_id = (string)record["client"];
            datumTijd = (string)record["datumTijd"];
            opmerkingen = (string)record["opmerkingen"];
        }
    }
}