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
        private string pathTypy = "typ.csv";
        private string pathAdresy = "adresy.csv";

        public bool Kontrola
        {
            get { return kontrola; }
            set { kontrola = value; }
        }

       public string PathTypy
        {
            get { return pathTypy; }
            set { pathTypy = value; }
        }

        public string PathAdresy
        {
            get { return pathAdresy; }
            set
            {
                pathAdresy = value;
            }
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


        public void CentralMethod(string vloz)
        {
            try
            {
                connection = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"]);
                connection.Open();

                using (SqlCommand command = new SqlCommand(vloz, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                throw new Exception("Error with connecting to database :(");

            }
        }


        public void DefaultInsert()
        {
            List<string> typy = new List<string>(); 
            using (StreamReader reader = new StreamReader(PathTypy))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    typy.Add(line);
                }
            }

            List<string> adresy = new List<string>();
            using (StreamReader reader = new StreamReader(PathAdresy))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    adresy.Add(line);
                }
            }

            List<string> strings = new List<string>();
            string vlozZak1 = "insert into zakaznik(jmeno, prijmeni, email, adresa_id) values ('Jakub', 'Novak', 'jakub.novak@gmail.com', 1);";
            string vlozZak2 = "insert into zakaznik(jmeno, prijmeni, email, adresa_id) values ('Karel', 'Zeleny', 'karel.zeleny@gmail.com', 2);";
            string vlozZak3 = "insert into zakaznik(jmeno, prijmeni, email, adresa_id) values ('Vladimir', 'Modry', 'vlada.modry@gmail.com', 3);";

            string vlozAdr1 = "insert into adresa(ulice, psc, mesto) values ('"+adresy[0]+"', " + adresy[1] +", '"+adresy[2]+"');";
            string vlozAdr2 = "insert into adresa(ulice, psc, mesto) values ('" + adresy[3] +"', " + adresy[4] +", '" + adresy[5] +"');";
            string vlozAdr3 = "insert into adresa(ulice, psc, mesto) values ('" + adresy[6] +"', " + adresy[7] +", '" + adresy[8] +"');";

            string vlozTyp1 = "insert into typ(nazev) values('" + typy[0] +"')";
            string vlozTyp2 = "insert into typ(nazev) values('" + typy[1] +"')";
            string vlozTyp3 = "insert into typ(nazev) values('" + typy[2] +"')";

            string vlozProdukt1 = "insert into produkt(nazev, cena_ks, typ) values('Rohlik', 5, 1)";
            string vlozProdukt2 = "insert into produkt(nazev, cena_ks, typ) values('Meda', 200, 2)";
            string vlozProdukt3 = "insert into produkt(nazev, cena_ks, typ) values('AK-47', 7000, 3)";

            string vlozObj1 = "insert into objednvka(cislo_obj, datum, zakaznik_id, produkt_id, cena, zaplaceno) values (100, '2022-05-02', 1, 1, 5, 1);";
            string vlozObj2 = "insert into objednvka(cislo_obj, datum, zakaznik_id, produkt_id, cena, zaplaceno) values (101, '2022-07-11', 2, 2, 200, 0);";
            string vlozObj3 = "insert into objednvka(cislo_obj, datum, zakaznik_id, produkt_id, cena, zaplaceno) values (102, '2022-11-28', 3, 3, 7000, 1);";

            strings.Add(vlozZak1);
            strings.Add(vlozZak2);
            strings.Add(vlozZak3);
            strings.Add(vlozAdr1);
            strings.Add(vlozAdr2);
            strings.Add(vlozAdr3);
            strings.Add(vlozTyp1);
            strings.Add(vlozTyp2);
            strings.Add(vlozTyp3);
            strings.Add(vlozProdukt1);
            strings.Add(vlozProdukt2);
            strings.Add(vlozProdukt3);
            strings.Add(vlozObj1);
            strings.Add(vlozObj2);
            strings.Add(vlozObj3);


            for (int i = 0; i < strings.Count; i++)
            {
                CentralMethod(strings[i]);
            }
        }

        public string Read()
        {
            string vypis = "--------------------------------------READ--------------------------------------\n";
            vypis += this.ReadAddressFromDatabase() + "\n";
            vypis += this.ReadZakaznikFromDatabase() + "\n";
            vypis += this.ReadTypFromDatabase() + "\n";
            vypis += this.ReadProduktFromDatabase() + "\n";
            vypis += this.ReadObjFromDatabase() + "\n--------------------------------------------------------------------------------";
            return vypis;
        }


        /// <summary>
        /// CRUD Adresy
        /// </summary>
        /// <param name="a"></param>
        public void InsertAdresa(Adresa a)
        {
            string vlozAdr = "insert into adresa(ulice, psc, mesto) values ('" + a.Ulice + "', " + a.Psc + ", '" + a.Mesto + "');";
            this.CentralMethod(vlozAdr);
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
                vypis += a.ToString() + "\n";
            }
            return vypis;
        }

        public void UpdateAdresy(string novy)
        {
            string update = "update adresa set ulice = '" + novy + "' where adresa.id = 1;";
            this.CentralMethod(update);
        }

        public void DeleteAdresa()
        {
            string del = "delete from adresa;";
            this.CentralMethod(del);
        }


        /// <summary>
        /// CRUD Zakaznika
        /// </summary>
        /// <param name="z"></param>
        public void InsertZakaznik(Zakaznik z)
        {
            string vlozZak = "insert into zakaznik(jmeno, prijmeni, email, adresa_id) values ('" + z.Jmeno + "', '" + z.Prijmeni + "', '" + z.Email + "', " + z.Adresa_id + ");";
            this.CentralMethod(vlozZak);
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

        public void UpdateZakaznik(string novy)
        {
            string update = "update zakaznik set jmeno = '" + novy + "' where zakaznik.id = 1;";
            this.CentralMethod(update);
        }

        public void DeleteZakaznik()
        {
            string del = "delete from zakaznik;";
            this.CentralMethod(del);
        }


        /// <summary>
        /// CRUD Typu
        /// </summary>
        /// <param name="t"></param>
        public void InsertTypu(Typ t)
        {
            string vlozTyp = "insert into typ(nazev) values ('" + t.Nazev + "');";
            this.CentralMethod(vlozTyp);
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

        public void UpdateTyp(string novy)
        {
            string update = "update typ set nazev = '" + novy + "' where typ.id = 1;";
            this.CentralMethod(update);
        }

        public void DeleteTyp()
        {
            string del = "delete from typ;";
            this.CentralMethod(del);
        }


        /// <summary>
        /// CRUD Produktu
        /// </summary>
        /// <param name="p"></param>
        public void InsertProdukt(Produkt p)
        {
            string vlozProdukt = "insert into produkt(nazev, cena_ks, typ) values ('" + p.Nazev + "', " + p.Cena_ks + ", " + p.Typ + ");";
            this.CentralMethod(vlozProdukt);
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

        public void UpdateProdukt(string novy)
        {
            string update = "update produkt set nazev = '" + novy + "' where produkt.id = 1;";
            this.CentralMethod(update);
        }

        public void DeleteProdukt()
        {
            string del = "delete from produkt;";
            this.CentralMethod(del);
        }



        /// <summary>
        /// CRUD Objednavky
        /// </summary>
        /// <param name="o"></param>
        public void InsertObj(Objednavka o)
        {
            int bit = 0;
            if (o.Zaplaceno == true)
            {
                bit = 1;
            }
            string vlozObj = "insert into objednavka(cislo_obj, datum, zakaznik_id, produkt_id, cena, zaplaceno) values ("+o.Cislo_obj+", '"+o.Datum+"', "+o.Zakaznik_id+", "+o.Produkt_id+", "+o.Cena+", "+bit+");";
            this.CentralMethod(vlozObj);
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

        public void UpdateObjednavka(int novy)
        {
            string update = "update objednavka set cena = " + novy + " where objednavka.id = 1;";
            this.CentralMethod(update);
        }

        public void DeleteObjednavka()
        {
            string del = "delete from objednavka;";
            this.CentralMethod(del);
        }
    }
}
