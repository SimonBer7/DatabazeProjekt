using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazeProjekt
{
    internal class Evidence
    {
        private List<Zakaznik> zakaznici;
        private List<Adresa> adresy;
        private List<Typ> typy;
        private List<Produkt> produkty;
        private List<Objednavka> objednavky;

        public List<Zakaznik> Zakaznici
        {
            get { return zakaznici; }
            set { zakaznici = value;}
        }

        public List<Adresa> Adresy
        {
            get { return adresy; }
            set { adresy = value; }
        }

        public List<Typ> Typy
        {
            get { return typy; }
            set { typy = value; }   
        }

        public List<Produkt> Produkty
        {
            get { return produkty; }
            set
            {
                produkty = value;
            }
        }

        public List<Objednavka> Objednavky
        {
            get { return objednavky; }
            set
            {
                objednavky = value;
            }
        }

        public void AddZakaznik(Zakaznik z)
        {
            zakaznici.Add(z);
        }

        public void AddAdresa(Adresa a)
        {
            adresy.Add(a);
        }

        public void AddTyp(Typ t)
        {
            typy.Add(t);
        }

        public void AddProdukt(Produkt p)
        {
            produkty.Add(p);
        }

        public void AddObjednavka(Objednavka o)
        {
            objednavky.Add(o);
        }






    }
}
