
using System;
using System.IO.Packaging;

namespace DatabazeProjekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Databazovy Projekt"); 
           
            Console.WriteLine("Stiskněte Enter pro připojení do databáze...");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                keyInfo = Console.ReadKey();
            }
            Databaze databaze = new Databaze();
            if(databaze.CheckConnectu() == true )
            {
                Console.WriteLine("----------------------------------"+"Pripojeno"+"----------------------------------");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Error, with connecting to database");
                Environment.Exit(0);
            }
            bool byloVypsano = false;
            bool running = true;
            
            //hlavni cyklus
            while (running)
            {
                Console.Write("Vyber =>\n1) C(reate)\n2) R(ead)\n3) U(pdate)\n4) D(elete)\n5) RESET\n6) EXIT\n");
                int volba;
                do
                {
                    Console.Write("Vaše volba (1-6): ");
                } while (!int.TryParse(Console.ReadLine(), out volba) || volba < 1 || volba > 6);

                volba = volba - 1;      
                switch (volba)
                {
                    case 0:
                        //CREATE
                        Console.WriteLine("--------------------------------------CREATE--------------------------------------");
                        if (!byloVypsano)
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
                                Console.WriteLine("----------------------------------------------------------------------------------");
                            }
                            else
                            {
                                Console.Write("\nDo ktere tabulky chcete vkladat data =>\n1) Adresa\n2) Typ\n3) Zakaznik\n4) Produkt\n5) Objednavka\n");
                                int vyberVytvoreni;
                                do
                                {
                                    Console.Write("Vaše volba (1-5): ");
                                } while (!int.TryParse(Console.ReadLine(), out vyberVytvoreni) || vyberVytvoreni < 1 || vyberVytvoreni > 5);
                                if (vyberVytvoreni == 1)
                                {
                                    VytvareniAdresy(databaze);
                                }
                                if (vyberVytvoreni == 2)
                                {
                                    VytvareniTypu(databaze);
                                }
                                if (vyberVytvoreni == 3)
                                {
                                    VytvareniZakaznika(databaze);
                                }
                                if (vyberVytvoreni == 4)
                                {
                                    VytvareniProduktu(databaze);
                                }
                                if (vyberVytvoreni == 5)
                                {
                                    VytvareniObjednavky(databaze);
                                }
                                Console.WriteLine("----------------------------------------------------------------------------------");
                            }
                        }
                        else
                        {
                            Console.Write("\nDo ktere tabulky chcete vkladat data =>\n1) Adresa\n2) Typ\n3) Zakaznik\n4) Produkt\n5) Objednavka\n");
                            int vyberVytvoreni;
                            do
                            {
                                Console.Write("Vaše volba (1-5): ");
                            } while (!int.TryParse(Console.ReadLine(), out vyberVytvoreni) || vyberVytvoreni < 1 || vyberVytvoreni > 5);
                            if (vyberVytvoreni == 1)
                            {
                                VytvareniAdresy(databaze);
                            }
                            if (vyberVytvoreni == 2)
                            {
                                VytvareniTypu(databaze);
                            }
                            if (vyberVytvoreni == 3)
                            {
                                VytvareniZakaznika(databaze);
                            }
                            if (vyberVytvoreni == 4)
                            {
                                VytvareniProduktu(databaze);
                            }
                            if (vyberVytvoreni == 5)
                            {
                                VytvareniObjednavky(databaze);
                            }
                            Console.WriteLine("----------------------------------------------------------------------------------");
                        }
                        break;

                    case 1:
                        //READ
                        Console.WriteLine(databaze.Read());
                        break;


                    case 2:
                        //UPDATE
                        Console.WriteLine("-----------------------------------UPDATE-----------------------------------");
                        Console.Write("\nV ktere tabulce chcete provest update =>\n1) Adresa\n2) Typ\n3) Zakaznik\n4) Produkt\n5) Objednavka\n");
                        int vyberUpdatu;
                        do
                        {
                            Console.Write("Vaše volba (1-5): ");
                        } while (!int.TryParse(Console.ReadLine(), out vyberUpdatu) || vyberUpdatu < 1 || vyberUpdatu > 5);
                        if (vyberUpdatu == 1)
                        {
                            Console.WriteLine("Zadej novou ulici: ");
                            string novy = Console.ReadLine();
                            databaze.UpdateAdresy(novy);
                        }
                        if (vyberUpdatu == 2)
                        {
                            Console.Write("Zadej novy nazev: ");
                            string novy = Console.ReadLine();
                            databaze.UpdateTyp(novy);
                        }
                        if (vyberUpdatu == 3)
                        {
                            Console.Write("Zadej novy jmeno: ");
                            string novy = Console.ReadLine();
                            databaze.UpdateZakaznik(novy);
                        }
                        if (vyberUpdatu == 4)
                        {
                            Console.Write("Zadej novy nazev: ");
                            string novy = Console.ReadLine();
                            databaze.UpdateProdukt(novy);
                        }
                        if (vyberUpdatu == 5)
                        {
                            Console.Write("Zadej novou cenu: ");
                            int cena = Int32.Parse(Console.ReadLine());
                            databaze.UpdateObjednavka(cena);
                        }
                        Console.WriteLine("---------------------------------------------------------------------------");
                        break;


                    case 3:
                        //DELETE
                        Console.WriteLine("\n-----------------------------------DELETE-----------------------------------"+databaze.Read());
                        Console.Write("\nZ ktere tabulky chcete smazat data =>\n1) Adresa\n2) Typ\n3) Zakaznik\n4) Produkt\n5) Objednavka\n");
                        int vyberDeletu;
                        do
                        {
                            Console.Write("Vaše volba (1-5): ");
                        } while (!int.TryParse(Console.ReadLine(), out vyberDeletu) || vyberDeletu < 1 || vyberDeletu > 5);
                        if (vyberDeletu == 1)
                        {
                            databaze.DeleteAdresa();
                        }
                        if (vyberDeletu == 2)
                        {
                            databaze.DeleteTyp();
                        }
                        if (vyberDeletu == 3)
                        {
                            databaze.DeleteZakaznik();
                        }
                        if (vyberDeletu == 4)
                        {
                            databaze.DeleteProdukt();
                        }
                        if (vyberDeletu == 5)
                        {
                            databaze.DeleteObjednavka();
                        }
                        Console.WriteLine("-------------------------------------------------------------------------------");
                        break;


                    case 4:
                        //RESET
                        databaze.ResetTables();
                        Console.WriteLine("-------------------------------------------------------------------------------");
                        break;


                    case 5:
                        //EXIT
                        Console.WriteLine("Ukoncili jste program.");
                        running = false;
                        break;
                }
            }
        }


        public static void VytvareniZakaznika(Databaze d)
        {
            Console.Write("\nZadej jmeno: ");
            string jmeno = Console.ReadLine();
            while (jmeno.Length < 3)
            {
                jmeno = Console.ReadLine();
            }
            Console.Write("\nZadej prijmeni: ");
            string prijmeni = Console.ReadLine();
            while (prijmeni.Length < 3)
            {
                prijmeni = Console.ReadLine();
            }
            Console.Write("\nZadej email: ");
            string email = Console.ReadLine();
            while (email.Length < 3)
            {
                email = Console.ReadLine();
            }
            int adresa_id;
            while (true)
            {
                Console.Write("\nZadej id adresy: ");

                if (int.TryParse(Console.ReadLine(), out adresa_id) && adresa_id > 0)
                {
                    break;
                }
            }
            Zakaznik z = new Zakaznik(jmeno, prijmeni, email, adresa_id);
            d.InsertZakaznik(z);    
        }

        public static void VytvareniAdresy(Databaze d)
        {
            Console.Write("\nZadej ulici: ");
            string ulice = Console.ReadLine();
            while(ulice.Length < 3)
            {
                ulice = Console.ReadLine();
            }
            int psc;
            while (true)
            {
                Console.Write("\nZadej psc: ");

                if (int.TryParse(Console.ReadLine(), out psc) && psc > 9999)
                {
                    break;
                }
            }
            Console.Write("\nZadej mesto: ");
            string mesto = Console.ReadLine();
            while (mesto.Length < 3)
            {
                mesto = Console.ReadLine();
            }
            Adresa a = new Adresa(ulice, psc, mesto);
            d.InsertAdresa(a);
        }

        public static void VytvareniProduktu(Databaze d)
        {
            Console.Write("\nZadej nazev: ");
            string nazev = Console.ReadLine();
            while (nazev.Length < 3)
            {
                nazev = Console.ReadLine();
            }
            Console.Write("Zadej cenu na kus: ");
            string cena_ks = Console.ReadLine();    
            int typ;
            while (true)
            {
                Console.Write("\nZadej id typu: ");

                if (int.TryParse(Console.ReadLine(), out typ) && typ > 0)
                {
                    break;
                }
            }

            Produkt p = new Produkt(nazev, cena_ks, typ);
            d.InsertProdukt(p);
        }


        public static void VytvareniTypu(Databaze d)
        {
            Console.Write("\nZadej nazev: ");
            string nazev = Console.ReadLine();
            while (nazev.Length < 3)
            {
                nazev = Console.ReadLine();
            }
            Typ t = new Typ(nazev);
            d.InsertTypu(t);
        }

        public static void VytvareniObjednavky(Databaze d) 
        {
            int cislo_obj;
            while (true)
            {
                Console.Write("\nZadej cislo objednavky: ");

                if (int.TryParse(Console.ReadLine(), out cislo_obj) && cislo_obj > 0)
                {
                    break;
                }
            }
            Console.Write("\nZadej datum objednavky (YYYY-MM-DD): ");
            DateTime datum = DateTime.Parse(Console.ReadLine());
            int zakaznik_id;
            while (true)
            {
                Console.Write("\nZadej id zakaznika: ");

                if (int.TryParse(Console.ReadLine(), out zakaznik_id) && zakaznik_id > 0)
                {
                    break;
                }
            }
            int produkt_id;
            while (true)
            {
                Console.Write("\nZadej id produktu: ");

                if (int.TryParse(Console.ReadLine(), out produkt_id) && produkt_id > 0)
                {
                    break;
                }
            }
            Console.Write("\nZadej cenu objednavky: ");
            string cena = Console.ReadLine();
            Console.Write("\nZadej zaplaceno True/False: ");
            bool zaplaceno = bool.Parse(Console.ReadLine());    
            Objednavka o = new Objednavka(cislo_obj, datum, zakaznik_id, produkt_id, cena, zaplaceno);
            d.InsertObj(o);
        }

    }
}