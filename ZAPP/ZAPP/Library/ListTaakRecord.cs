using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ZAPP
{
    public class ListTaakRecord
    {
        public string id;
        public string zorgmoment_id;
        public string stap;
        public string omschrijving;
        public string voltooid;

        public ListTaakRecord (string id, string zorgmoment_id, string stap, string omschrijving, string voltooid)
        {
            this.id = id;
            this.zorgmoment_id = zorgmoment_id;
            this.stap = stap;
            this.omschrijving = omschrijving;
            this.voltooid = voltooid;
        }
    }
}