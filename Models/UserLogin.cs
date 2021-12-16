using annuaire.Tools;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace annuaire.Models
{
    public class UserLogin
    {
        private int userId;
        private string username;
        private string password;
        private static string request;
        private static MySqlCommand command;
        private static MySqlConnection connection;
        private static MySqlDataReader reader;

        public int UserId { get => userId; set => userId = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }

        public static bool CheckAuthentification(UserLogin user)
        {
            int result = 0;
            request = "SELECT COUNT(*) FROM user_login WHERE user_name = @UserName AND user_password = @UserPassword";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@UserName", user.Username));
            command.Parameters.Add(new MySqlParameter("@UserPassword", user.Password));
            connection.Open();
            reader = command.ExecuteReader();

            if (reader.Read())
            {
                result = reader.GetInt32(0);
            }

            reader.Close();
            command.Dispose();
            connection.Close();

            return result == 1;

        }
    }
}
