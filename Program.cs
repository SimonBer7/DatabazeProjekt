
using System.IO.Packaging;

namespace DatabazeProjekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
           
            Console.WriteLine("Stiskněte Enter pro připojení do databáze...");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                keyInfo = Console.ReadKey();
            }

            Databaze databaze = new Databaze();
            Evidence evidence = new Evidence();
            if(databaze.CheckConnectu() == true )
            {
                Console.WriteLine("----------------------------------"+"Pripojeno" +"----------------------------------");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Error, with connecting to database");
                Environment.Exit(0);
            }

            Console.Write("Vyber =>\n1) C(reate)\n2) R(ead)\n3) U(pdate)\n4) D(elete)\n");
            int volba;
            do
            {
                Console.Write("Vaše volba (1-4): ");
            } while (!int.TryParse(Console.ReadLine(), out volba) || volba < 1 || volba > 4);

            volba = volba - 1;
            bool byloVypsano = false;

            switch (volba)
            {
                case 0:
                    //CREATE
                    if(byloVypsano == false)
                    {
                        Console.WriteLine("\n1) Default insert\n2) Vlastni insert\n");
                        int vyber;
                        do
                        {
                            Console.Write("Vaše volba (1/2): ");
                        } while (!int.TryParse(Console.ReadLine(), out vyber) || vyber < 1 || vyber > 2);
                        if (vyber == 1)
                        {
                            databaze.DefaultInsert();
                            byloVypsano = true;
                        }
                        else
                        {
                            Console.Write("\nDo ktere tabulky chcete vkladat data =>\n1) Zakaznik\n2) Adresa\n3) Produkt\n4) Typ\n5) Objednavka\n");
                            int vyberVytvoreni;
                            do
                            {
                                Console.Write("Vaše volba (1-5): ");
                            } while (!int.TryParse(Console.ReadLine(), out vyberVytvoreni) || vyberVytvoreni < 1 || vyberVytvoreni > 5);
                            if (vyberVytvoreni == 1)
                            {
                                VytvareniZakaznika(databaze, evidence);
                            }
                            if (vyberVytvoreni == 2)
                            {
                                VytvareniAdresy(databaze, evidence);
                            }
                            if (vyberVytvoreni == 3)
                            {
                                VytvareniProduktu(databaze, evidence);
                            }
                            if (vyberVytvoreni == 4)
                            {
                                VytvareniTypu(databaze, evidence);
                            }
                            if (vyberVytvoreni == 5)
                            {
                                VytvareniObjednavky(databaze, evidence);
                            }
                        }
                    }
                    break;

                case 1:
                    //READ
                    Console.WriteLine(databaze.Read());
                    break;


                case 2:
                    //UPGRADE
                    break;

                case 3:
                    //DELETE



                    break;
            }




        }


        public static void VytvareniZakaznika(Databaze d, Evidence e)
        {
            Console.Write("\nZadej jmeno: ");
            string jmeno = Console.ReadLine();
            Console.Write("\nZadej prijmeni: ");
            string prijmeni = Console.ReadLine();
            Console.Write("\nZadej email: ");
            string email = Console.ReadLine();
            Console.Write("\nZadej id adresy: ");
            int adresa_id = Int32.Parse(Console.ReadLine());

            Zakaznik z = new Zakaznik(jmeno, prijmeni, email, adresa_id);
            e.AddZakaznik(z);
            d.InsertZakaznik(z);    


        }

        public static void VytvareniAdresy(Databaze d, Evidence e)
        {
            Console.Write("\nZadej ulici: ");
            string ulice = Console.ReadLine();
            Console.Write("\nZadej psc: ");
            int psc = Int32.Parse(Console.ReadLine());
            Console.Write("\nZadej mesto: ");
            string mesto = Console.ReadLine();

            Adresa a = new Adresa(ulice, psc, mesto);
            e.AddAdresa(a);
            d.InsertAdresa(a);
        }

        public static void VytvareniProduktu(Databaze d, Evidence e)
        {
            Console.Write("\nZadej nazev: ");
            string nazev = Console.ReadLine();
            Console.Write("Zadej cenu na kus: ");
            float cena_ks = float.Parse(Console.ReadLine());    
            Console.Write("Zadej id typu: ");
            int typ = Int32.Parse(Console.ReadLine());

            Produkt p = new Produkt(nazev, cena_ks, typ);
            e.AddProdukt(p);
            d.InsertProdukt(p);
        }


        public static void VytvareniTypu(Databaze d, Evidence e)
        {
            Console.Write("\nZadej nazev: ");
            string nazev = Console.ReadLine();

            Typ t = new Typ(nazev);
            e.AddTyp(t);
            d.InsertTypu(t);
        }

        public static void VytvareniObjednavky(Databaze d, Evidence e) 
        {
            Console.Write("\nZadej cislo objednavky: ");
            int cislo_obj = Int32.Parse(Console.ReadLine());
            Console.Write("\nZadej datum objednavky (YYYY-MM-DD): ");
            DateTime datum = DateTime.Parse(Console.ReadLine());
            Console.Write("\nZadej id zakaznika: ");
            int zakaznik_id = Int32.Parse(Console.ReadLine());
            Console.Write("\nZadej id produktu: ");
            int produkt_id = Int32.Parse(Console.ReadLine());
            Console.Write("\nZadej cenu objednavky: ");
            float cena = float.Parse(Console.ReadLine());
            Console.Write("\nZadej zaplaceno True/False: ");
            bool zaplaceno = bool.Parse(Console.ReadLine());

            Objednavka o = new Objednavka(cislo_obj, datum, zakaznik_id, produkt_id, cena, zaplaceno);
            e.AddObjednavka(o);
            d.InsertObj(o);
        }

    }
}