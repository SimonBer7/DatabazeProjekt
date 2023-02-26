using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DatabazeProjekt
{
    internal class Databaze
    {

        private static SqlConnection connection = null;
        private bool kontrola = false;

        public bool Kontrola
        {
            get { return kontrola; }
            set { kontrola = value; }
        }

        public Databaze()
        {
            try
            {
                connection = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"]);
                connection.Open();
                Kontrola = true;
            }
            catch {
                throw new Exception("Error with connecting to database :(");

            }
           

        }
        public bool CheckConnectu()
        {
            if(Kontrola == true) { return true; } else
            {
                return false;
            }
        }

        private static string ReadSettings(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[key] ?? "Not Found";
            return result;
        }

        public static void CloseConnection()
        {
            try
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            catch { }
            finally
            {
                connection = null;
            }
        }

        public void DefaultInsert()
        {

            /*
            List<string> list = new List<string>(); 
            using (var reader = new StreamReader("typ.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                // Nastavení oddělovače sloupců a hlavičky souboru
                csv.Configuration.Delimiter = ",";
                csv.Configuration.HasHeaderRecord = true;

                // Načtení dat ze souboru
                var records = csv.GetRecords<Typ>();

                // Vypsání názvů sloupců
                var headerRecord = csv.Context.HeaderRecord;
                Console.WriteLine(string.Join(", ", headerRecord));

                // Vypsání hodnot jednotlivých sloupců v každém řádku souboru
                foreach (var record in records)
                {
                    list.Add(record.ToString());
                    
                }
            }
            */
            string vlozZak1 = "insert into zakaznik(jmeno, prijmeni, email, adresa_id) values ('Jakub', 'Novak', 'jakub.novak@gmail.com', 1);";
            string vlozZak2 = "insert into zakaznik(jmeno, prijmeni, email, adresa_id) values ('Karel', 'Zeleny', 'karel.zeleny@gmail.com', 2);";
            string vlozZak3 = "insert into zakaznik(jmeno, prijmeni, email, adresa_id) values ('Vladimir', 'Modry', 'vlada.modry@gmail.com', 3);";

            string vlozAdr1 = "insert into adresa(ulice, psc, mesto) values ('V ulicce 505', 12121, 'Olomouc');";
            string vlozAdr2 = "insert into adresa(ulice, psc, mesto) values ('Na vyhlidce 201', 13101, 'Pelhrimov');";
            string vlozAdr3 = "insert into adresa(ulice, psc, mesto) values ('K Posedu 55', 09133, 'Karvina');";

            string vlozTyp1 = "insert into typ(nazev) values('jidlo')";
            string vlozTyp2 = "insert into typ(nazev) values('hracka')";
            string vlozTyp3 = "insert into typ(nazev) values('zbran')";

            string vlozProdukt1 = "insert into produkt(nazev, cena_ks, typ) values('Rohlik', 5, 1)";
            string vlozProdukt2 = "insert into produkt(nazev, cena_ks, typ) values('Meda', 200, 2)";
            string vlozProdukt3 = "insert into produkt(nazev, cena_ks, typ) values('AK-47', 7000, 3)";

            string vlozObj1 = "insert into objednvka(cislo_obj, datum, zakaznik_id, produkt_id, cena, zaplaceno) values (100, '2022-05-02', 1, 1, 5, 1);";
            string vlozObj2 = "insert into objednvka(cislo_obj, datum, zakaznik_id, produkt_id, cena, zaplaceno) values (101, '2022-07-11', 2, 2, 200, 0);";
            string vlozObj3 = "insert into objednvka(cislo_obj, datum, zakaznik_id, produkt_id, cena, zaplaceno) values (102, '2022-11-28', 3, 3, 7000, 1);";

            using (SqlCommand command = new SqlCommand(vlozZak1, connection))
            {
                command.ExecuteNonQuery();

            }

            using (SqlCommand command = new SqlCommand(vlozZak2, connection))
            {
                command.ExecuteNonQuery();

            }

            using (SqlCommand command = new SqlCommand(vlozZak3, connection))
            {
                command.ExecuteNonQuery();

            }

            using (SqlCommand command = new SqlCommand(vlozAdr1, connection))
            {
                command.ExecuteNonQuery();

            }

            using (SqlCommand command = new SqlCommand(vlozAdr2, connection))
            {
                command.ExecuteNonQuery();

            }

            using (SqlCommand command = new SqlCommand(vlozAdr3, connection))
            {
                command.ExecuteNonQuery();

            }

            using (SqlCommand command = new SqlCommand(vlozTyp1, connection))
            {
                command.ExecuteNonQuery();

            }

            using (SqlCommand command = new SqlCommand(vlozTyp2, connection))
            {
                command.ExecuteNonQuery();

            }

            using (SqlCommand command = new SqlCommand(vlozTyp3, connection))
            {
                command.ExecuteNonQuery();

            }

            using (SqlCommand command = new SqlCommand(vlozProdukt1, connection))
            {
                command.ExecuteNonQuery();

            }

            using (SqlCommand command = new SqlCommand(vlozProdukt2, connection))
            {
                command.ExecuteNonQuery();

            }

            using (SqlCommand command = new SqlCommand(vlozProdukt3, connection))
            {
                command.ExecuteNonQuery();

            }

            using (SqlCommand command = new SqlCommand(vlozObj1, connection))
            {
                command.ExecuteNonQuery();

            }


            using (SqlCommand command = new SqlCommand(vlozObj2, connection))
            {
                command.ExecuteNonQuery();

            }

            using (SqlCommand command = new SqlCommand(vlozObj3, connection))
            {
                command.ExecuteNonQuery();

            }
        }


        public void InsertZakaznik(Zakaznik z)
        {
            string vlozZak = "insert into zakaznik(jmeno, prijmeni, email, adresa_id) values ('"+z.Jmeno+"', '"+z.Prijmeni+"', '"+z.Email+"', "+z.Adresa_id+");";
            using (SqlCommand command = new SqlCommand(vlozZak, connection))
            {
                command.ExecuteNonQuery();

            }
        }

        public void InsertAdresa(Adresa a)
        {
            string vlozAdr = "insert into adresa(ulice, psc, mesto) values ('" + a.Ulice + "', " + a.Psc + ", '" + a.Mesto + "');";
            using (SqlCommand command = new SqlCommand(vlozAdr, connection))
            {
                command.ExecuteNonQuery();

            }
        }

        public void InsertProdukt(Produkt p) 
        {
            string vlozProdukt = "insert into produkt(nazev, cena_ks, typ) values ('"+p.Nazev+"', "+p.Cena_ks+", "+p.Typ+");";
            using (SqlCommand command = new SqlCommand(vlozProdukt, connection))
            {
                command.ExecuteNonQuery();

            }
        }

        public void InsertTypu(Typ t)
        {
            string vlozTyp = "insert into typ(nazev) values ('" + t.Nazev + "');";
            using (SqlCommand command = new SqlCommand(vlozTyp, connection))
            {
                command.ExecuteNonQuery();

            }
        }


        public void InsertObj(Objednavka o)
        {
            int bit = 0;
            if (o.Zaplaceno == true)
            {
                bit = 1;
            }
            string vlozObj = "insert into objednvka(cislo_obj, datum, zakaznik_id, produkt_id, cena, zaplaceno) values ("+o.Cislo_obj+", '"+o.Datum+"', "+o.Zakaznik_id+", "+o.Produkt_id+", "+o.Cena+", "+bit+");";
        }


        public string ReadAddressFromDatabase()
        {
            string vypis = "";
            string selectAdresy = "select * from adresa;";
            using SqlCommand command = new SqlCommand(selectAdresy, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ulice = (string)reader["ulice"];
                int psc = (int)reader["psc"];
                string mesto = (string)reader["mesto"];
                

                Adresa a = new Adresa(ulice, psc, mesto);   
                vypis += a.ToString()+"\n";
            }
            return vypis;
        }


        public string ReadZakaznikFromDatabase()
        {
            string vypis = "";
            string selectAdresy = "select * from zakaznik;";
            using SqlCommand command = new SqlCommand(selectAdresy, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string jmeno = (string)reader["jmeno"];
                string prijmeni = (string)reader["prijmeni"];
                string email = (string)reader["email"];
                int adresa_id = (int)reader["adresa_id"];

                Zakaznik z = new Zakaznik(jmeno, prijmeni, email, adresa_id);                 
                vypis += z.ToString() + "\n";
            }
            return vypis;
        }


        public string ReadTypFromDatabase()
        {
            string vypis = "";
            string selectAdresy = "select * from typ;";
            using SqlCommand command = new SqlCommand(selectAdresy, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string nazev = (string)reader["nazev"];

                Typ t = new Typ(nazev);
                vypis += t.ToString() + "\n";
            }
            return vypis;
        }

        public string ReadProduktFromDatabase()
        {
            string vypis = "";
            string selectAdresy = "select * from produkt;";
            using SqlCommand command = new SqlCommand(selectAdresy, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string nazev = (string)reader["nazev"];
                float cena_ks = (float)reader["cena_ks"];
                int typ = (int)reader["typ"];

                Produkt p = new Produkt(nazev, cena_ks, typ);
                vypis += p.ToString() + "\n";
            }
            return vypis;
        }

        public string ReadObjFromDatabase()
        {
            string vypis = "";
            string selectAdresy = "select * from objednavka;";
            using SqlCommand command = new SqlCommand(selectAdresy, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            { 
                int cislo_obj = (int)reader["cislo_obj"];
                DateTime datum = (DateTime)reader["datum"];
                int zakaznik_id = (int)reader["zakaznik_id"];
                int produkt_id = (int)reader["produkt_id"];
                float cena = (float)reader["cena"];
                bool zaplaceno = (bool)reader["zaplaceno"];

                Objednavka o = new Objednavka(cislo_obj, datum, zakaznik_id, produkt_id, cena, zaplaceno);
                vypis += o.ToString() + "\n";
            }
            return vypis;
        }

        public string Read()
        {
            string vypis = "";
            vypis += this.ReadAddressFromDatabase()+"\n";
            vypis += this.ReadZakaznikFromDatabase() + "\n";
            vypis += this.ReadTypFromDatabase()+"\n";
            vypis += this.ReadProduktFromDatabase() + "\n";
            vypis += this.ReadObjFromDatabase() + "\n";
            return vypis;
        }



    }
}
