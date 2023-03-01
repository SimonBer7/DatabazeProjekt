using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazeProjekt
{
    internal class Zakaznik
    {
        private string jmeno;
        private string prijmeni;
        private string email;
        private int adresa_id;

        public string Jmeno
        {
            get { return jmeno; }
            set { jmeno = value; }
        }

        public string Prijmeni
        {
            get { return prijmeni; }
            set { prijmeni = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public int Adresa_id
        {
            get { return adresa_id; }
            set
            {
                adresa_id = value;
            }
        }

        public Zakaznik(string jm, string pr, string em, int adr)
        {
            Jmeno = jm;
            Prijmeni = pr;
            Email = em;
            Adresa_id = adr;
        }

        public override string ToString()
        {
            return "Zakaznik " + Jmeno + " " + Prijmeni + ", email: " + Email + ", adresa: "+Adresa_id;
        }
    }
}
