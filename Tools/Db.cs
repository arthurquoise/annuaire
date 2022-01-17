using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace annuaire.Tools
{
    public class Db
    {
        public Db()
        {
        }

        //Set the connection string in a variable
        private static string connectionString = "Server=127.0.0.1;DataBase=annuaire;UserId=root;password=";
        public static MySqlConnection Connection { get => new MySqlConnection(connectionString); }

    }
}
