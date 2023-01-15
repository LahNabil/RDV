using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDV
{
    internal class Connexion
    {
        public MySqlConnection conn;
        public String database;
        public String server;
        public String uid;
        public String password;
        public String connString;

        public Connexion()
        {
            server = "localhost";
            database = "rdv";
            uid = "root";
            password = "nabil";
            connString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

        }
        public MySqlConnection Conn()
        {
            conn = new MySqlConnection(connString);
            return conn;
        }
        public void seConnecter()
        {

            conn.Open();
        }
        public void seDeconnecter()
        {

            conn.Close();
        }
    }
}
