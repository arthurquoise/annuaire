using annuaire.Tools;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace annuaire.Models
{
    public class Site
    {
        private int siteId;
        private string siteName;
        private string siteType;
        private static string request;
        private static MySqlCommand command;
        private static MySqlConnection connection;
        private static MySqlDataReader reader;

        public int SiteId { get => siteId; set => siteId = value; }
        public string SiteName { get => siteName; set => siteName = value; }
        public string SiteType { get => siteType; set => siteType = value; }

        public Site()
        {

        }

        //Save the site un the database
        public bool Save()
        {
            request = "INSERT INTO site (site_name, site_type) values (@SiteName, @SiteType); SELECT LAST_INSERT_ID()";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@SiteName", SiteName));
            command.Parameters.Add(new MySqlParameter("@SiteType", SiteType));
            connection.Open();
            SiteId = Convert.ToInt32(command.ExecuteScalar());
            command.Dispose();
            connection.Close();
            return SiteId > 0;
        }

        //Update site in database
        public bool SaveModifications()
        {
            request = "UPDATE site SET site_name = @SiteName, site_type = @SiteType WHERE site_id = @SiteId";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@SiteName", SiteName));
            command.Parameters.Add(new MySqlParameter("@SiteType", SiteType));
            command.Parameters.Add(new MySqlParameter("@SiteId", SiteId));
            connection.Open();
            int response = command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            return response == 1;
        }

        //Delete the site from the database
        public bool Delete()
        {
            request = "DELETE FROM site where site_id=@SiteId";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@SiteId", SiteId));
            connection.Open();
            int nb = command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            return nb == 1;
        }

        //Check if employees are affected to the site
        public bool DeleteVerification()
        {
            int result = -1;
            request = "SELECT COUNT(*) FROM employee WHERE site_id = @SiteId";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@SiteId", SiteId));
            connection.Open();
            reader = command.ExecuteReader();

            if (reader.Read())
            {
                result = reader.GetInt32(0);
            }

            reader.Close();
            command.Dispose();
            connection.Close();

            return result == 0;
        }

        //Get one site by id
        public static Site GetSite(int id)
        {
            Site site = null;

            request = "SELECT site_id, site_name, site_type FROM site WHERE site_id = @SiteId";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("SiteId", id));
            connection.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                site = new Site()
                {
                    SiteId = reader.GetInt32(0),
                    SiteName = reader.GetString(1),
                    SiteType = reader.GetString(2),
                };
            }
            reader.Close();
            command.Dispose();
            connection.Close();

            return site;
        }

        //Get the full sites list
        public static List<Site> GetSites()
        {
            List<Site> sites = new List<Site>();
            request = "SELECT site_id, site_name, site_type FROM site ORDER BY site_name";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            connection.Open();
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Site site = new Site()
                {
                    SiteId = reader.GetInt32(0),
                    siteName = reader.GetString(1),
                    SiteType = reader.GetString(2)
                };
                sites.Add(site);
            }

            reader.Close();
            command.Dispose();
            connection.Close();
            return sites;
        }
    }
}
