using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazeProjekt
{
    internal class Adresa
    {

        private string ulice;
        private int psc;
        private string mesto;


        public string Ulice
        {
            get { return ulice; }
            set { ulice = value; }
        }

        public int Psc
        {
            get { return psc; }
            set { psc = value; }
        }

        public string Mesto
        {
            get { return mesto; }
            set { mesto = value; }
        }

        public Adresa(string ul, int psc, string me)
        {
            Ulice = ul;
            Psc = psc;
            Mesto = me;
        }


        public override string ToString()
        {
            return "Adresa: " + Ulice + ", " + Psc + ", " + Mesto;
        }

    }
}
