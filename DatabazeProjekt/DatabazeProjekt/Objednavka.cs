using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazeProjekt
{
    internal class Objednavka
    {
        private int cislo_obj;
        private DateTime datum;
        private int zakaznik_id;
        private int produkt_id;
        private string cena;
        private bool zaplaceno;

        public int Cislo_obj
        {
            get { return cislo_obj; }
            set { cislo_obj = value;}
        }

        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }

        public int Zakaznik_id
        {
            get { return zakaznik_id; }
            set
            {
                zakaznik_id = value;
            }
        }

        public int Produkt_id
        {
            get { return produkt_id; }
            set
            {
                produkt_id = value;
            }
        }

        public string Cena
        {
            get { return cena; }
            set
            {
                cena = value;
            }
        }

        public bool Zaplaceno
        {
            get { return zaplaceno; }
            set
            {
                zaplaceno = value;
            }
        }

        public Objednavka(int cis, DateTime dat, int zak, int prod, string cen, bool zap)
        {
            Cislo_obj = cis;
            Datum = dat;
            Zakaznik_id = zak;
            Produkt_id = prod;
            Cena = cen;
            Zaplaceno = zap;
        }

        public override string ToString()
        {
            return "Objednavka č." + Cislo_obj + ", datum: " + Datum.ToLongDateString() + ", zakaznik id: " + Zakaznik_id + ", produkt id: " + Produkt_id + ", cena: " + Cena + ", zaplaceno: " + Zaplaceno;
        }
    }
}
