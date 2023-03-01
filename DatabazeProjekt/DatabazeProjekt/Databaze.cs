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

        /// <summary>
        /// metoda na executovani prichozich stringu(prikazu)
        /// </summary>
        /// <param name="vloz"></param>
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

            catch (SqlException ex)
            {
                Console.WriteLine("Error with database: " + ex.Message);
            }
        }

        /// <summary>
        /// metoda, ve ktere smazu vsechny tably a nasledne je vytvorim (nastavim si tak hezky id od zacatku)
        /// </summary>
        public void ResetTables()
        {
            List<string> drops = new List<string>();
            List<string> creates = new List<string>();
            string dropAdresa = "drop table adresa;";
            string dropZakaznik = "drop table zakaznik;";
            string dropTyp = "drop table typ;";
            string dropProdukt = "drop table Produkt;";
            string dropObj = "drop table Objednavka;";

            string createAdresa = "create table adresa(id int primary key identity(1,1),ulice varchar(30) not null,psc int not null,mesto varchar(30) not null);";
            string createZakaznik = "create table zakaznik(id int primary key identity(1,1),jmeno varchar(20) not null,prijmeni varchar(20) not null,email varchar(50) not null,adresa_id int foreign key references adresa(id));";
            string createTyp = "create table typ(id int primary key identity(1,1),nazev varchar(30) not null);";
            string createProdukt = "create table produkt(id int primary key identity(1,1),nazev varchar(30) not null,cena_ks float not null check(cena_ks > 0),typ int not null foreign key references typ(id));";
            string createObj = "create table objednavka(id int primary key identity(1,1),cislo_obj int not null check(cislo_obj > 0),datum date not null default(format (getdate(), 'yyyy-MM-dd')),zakaznik_id int foreign key references zakaznik(id),produkt_id int foreign key references produkt(id),cena float not null check(cena > 0),zaplaceno bit not null);";
            
            drops.Add(dropObj);
            drops.Add(dropProdukt);
            drops.Add(dropZakaznik);
            drops.Add(dropTyp);
            drops.Add(dropAdresa);

            creates.Add(createAdresa);
            creates.Add(createTyp);
            creates.Add(createZakaznik);
            creates.Add(createProdukt);
            creates.Add (createObj);
           
            for(int i = 0; i < drops.Count; i++)
            {
                this.CentralMethod(drops[i]);
            }

            for (int i = 0; i < creates.Count; i++)
            {
                this.CentralMethod(creates[i]);
            }

        }

        /// <summary>
        /// metoda, ve ktere nacitam data z csv souboru a vkladamje do databaze
        /// </summary>
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
                    line.Split(",").ToList().ForEach(x => adresy.Add(x));
                    
                }
            }

            List<string> strings = new List<string>();
          
            string vlozAdr1 = "insert into adresa(ulice, psc, mesto) values ('"+adresy[0]+"', " + adresy[1] +", '"+adresy[2]+"');";
            string vlozAdr2 = "insert into adresa(ulice, psc, mesto) values ('" + adresy[3] +"', " + adresy[4] +", '" + adresy[5] +"');";
            string vlozAdr3 = "insert into adresa(ulice, psc, mesto) values ('" + adresy[6] +"', " + adresy[7] +", '" + adresy[8] +"');";
            string vlozTyp1 = "insert into typ(nazev) values('" + typy[0] +"')";
            string vlozTyp2 = "insert into typ(nazev) values('" + typy[1] +"')";
            string vlozTyp3 = "insert into typ(nazev) values('" + typy[2] +"')";

            strings.Add(vlozAdr1);
            strings.Add(vlozAdr2);
            strings.Add(vlozAdr3);
            strings.Add(vlozTyp1);
            strings.Add(vlozTyp2);
            strings.Add(vlozTyp3);
            
            for (int i = 0; i < strings.Count; i++)
            {
                CentralMethod(strings[i]);
            }
        }

        /// <summary>
        /// metoda, ktera slouzi pro vypis vsech dat z jednotlivych tabulek
        /// </summary>
        /// <returns></returns>
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
            string vlozZak = "insert into zakaznik(jmeno, prijmeni, email, adresa_id) values ('" + z.Jmeno + "', '" + z.Prijmeni + "', '" + z.Email + "', " + z.Adresa_id + ");";
            using (SqlCommand command = new SqlCommand(vlozZak, connection))
            {
                command.Parameters.AddWithValue("@jmeno", z.Jmeno);
                command.Parameters.AddWithValue("@prijmeni", z.Prijmeni);
                command.Parameters.AddWithValue("@email", z.Email);
                command.Parameters.AddWithValue("@adresa_id", z.Adresa_id);
                this.CentralMethod(vlozZak);
            }
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
            using (SqlCommand command = new SqlCommand(vlozProdukt, connection))
            {
                command.Parameters.AddWithValue("@nazev", p.Nazev);
                command.Parameters.AddWithValue("@cena_ks", p.Cena_ks);
                command.Parameters.AddWithValue("@typ", p.Typ);
                this.CentralMethod(vlozProdukt);
            }
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
                double cena_ks = (double)reader["cena_ks"];
                int typ = (int)reader["typ"];
                string cena = cena_ks.ToString();
                Produkt p = new Produkt(nazev, cena, typ);
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
            string vlozObj = "insert into objednavka(cislo_obj, datum, zakaznik_id, produkt_id, cena, zaplaceno) values ("+o.Cislo_obj+", '"+o.Datum.ToString("yyyy-MM-dd")+"', "+o.Zakaznik_id+", "+o.Produkt_id+", "+o.Cena+", "+bit+");";
            using (SqlCommand command = new SqlCommand(vlozObj, connection))
            {
                command.Parameters.AddWithValue("@cislo_obj", o.Cislo_obj);
                command.Parameters.AddWithValue("@datum", o.Datum);
                command.Parameters.AddWithValue("@zakaznik_id", o.Zakaznik_id);
                command.Parameters.AddWithValue("@produkt_id", o.Produkt_id);
                command.Parameters.AddWithValue("@cena", o.Cena);
                command.Parameters.AddWithValue("@zaplaceno", o.Zaplaceno);
                this.CentralMethod(vlozObj);
            }
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
                double cena = (double)reader["cena"];
                bool zaplaceno = (bool)reader["zaplaceno"];
                string cena_celkem = cena.ToString();
                Objednavka o = new Objednavka(cislo_obj, datum, zakaznik_id, produkt_id, cena_celkem, zaplaceno);
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