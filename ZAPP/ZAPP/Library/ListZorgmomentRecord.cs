namespace ZAPP
{
    public class ListZorgmomentRecord
    {
        public string id;
        public string client_id;
        public string datum_tijd;
        public string opmerkingen;
        public string aanwezigheid_begin;
        public string aanwezigheid_eind;
        public string nieuw;

        public ListZorgmomentRecord (string id, string client_id, string datum_tijd, string opmerkingen, string aanwezigheid_begin, string aanwezigheid_eind, string nieuw)
        {
            this.id = id;
            this.client_id = client_id;
            this.datum_tijd = datum_tijd;
            this.opmerkingen = opmerkingen;
            this.aanwezigheid_begin = aanwezigheid_begin;
            this.aanwezigheid_eind = aanwezigheid_eind;
            this.nieuw = nieuw;
        }
    }
}