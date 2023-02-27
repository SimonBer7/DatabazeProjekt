using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazeProjekt
{
    internal class Produkt
    {

        private string nazev;
        private float cena_ks;
        private int typ;

        public string Nazev
        {
            get { return nazev; }
            set { nazev = value; }
        }

        public float Cena_ks
        {
            get { return cena_ks; }
            set { cena_ks = value; }
        }

        public int Typ
        {
            get { return typ; }
            set { typ = value; }
        }

        public Produkt(string n, float cen, int tp)
        {
            Nazev = n;
            Cena_ks = cen;
            Typ = tp;
        }

        public override string ToString()
        {
            return "Produkt " + Nazev + ", cena za kus: " + Cena_ks + ", typ: " + Typ;
        }






    }
}
