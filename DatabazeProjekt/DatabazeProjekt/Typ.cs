using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazeProjekt
{
    internal class Typ
    {
        private string nazev;

        public string Nazev
        {
            get { return nazev; }
            set { nazev = value; }
        }

        public Typ(string n) 
        {
            Nazev = n;
        }

        public override string ToString()
        {
            return "Typ: "+Nazev;
        }
    }
}
