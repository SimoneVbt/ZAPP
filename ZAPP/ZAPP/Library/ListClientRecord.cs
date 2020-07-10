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
    public class ListClientRecord
    {
        public string id;
        public string achternaam;
        public string voornaam;
        public string adres;
        public string postcode;
        public string woonplaats;
        public string telefoonnummer;

        public ListClientRecord(string id, string achternaam, string voornaam, string adres, string postcode, string woonplaats, string telefoonnummer)
        {
            this.id = id;
            this.achternaam = achternaam;
            this.voornaam = voornaam;
            this.adres = adres;
            this.postcode = postcode;
            this.woonplaats = woonplaats;
            this.telefoonnummer = telefoonnummer;
        }
    }
}