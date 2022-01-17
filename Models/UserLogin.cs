using annuaire.Tools;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        [StringLength(15, ErrorMessage = "Wrong username", MinimumLength = 3)]
        public string Username { get => username; set => username = value; }

        [Required]
        public string Password { get => password; set => password = value; }

        //Check if the credentials are valid
        public static bool CheckAuthentification(UserLogin user)
        {
            int result = 0;
            user.password = HashPassword(user.password);
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
        //Hash the password with MD5
        private static string HashPassword(string password)
        {
            using MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
