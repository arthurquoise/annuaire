using annuaire.Tools;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace annuaire.Models
{
    public class Department
    {
        private int departmentId;
        private string departmentName;
        private static MySqlCommand command;
        private static string request;
        private static MySqlConnection connection;
        private static MySqlDataReader reader;

        public int DepartmentId { get => departmentId; set => departmentId = value; }

        [Required]
        [StringLength(50, ErrorMessage = "Compris entre 3 et 50 caractères", MinimumLength = 3)]
        public string DepartmentName { get => departmentName; set => departmentName = value; }

        public Department()
        {

        }

        public bool Save()
        {
            request = "INSERT INTO department (department_name) values (@DepartmentName); SELECT LAST_INSERT_ID()";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@DepartmentName", DepartmentName));
            connection.Open();
            DepartmentId = Convert.ToInt32(command.ExecuteScalar());
            command.Dispose();
            connection.Close();
            return DepartmentId > 0;
        }

        //Update site in database
        public bool SaveModifications()
        {
            request = "UPDATE department SET department_name = @DepartmentName WHERE department_id = @DepartmentId";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@DepartmentName", DepartmentName));
            command.Parameters.Add(new MySqlParameter("@DepartmentId", DepartmentId));
            connection.Open();
            int response = command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            return response == 1;
        }

        //Delete the site from the database
        public bool Delete()
        {
            request = "DELETE FROM department where department_id=@DepartmentId";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@DepartmentId", DepartmentId));
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
            request = "SELECT COUNT(*) FROM employee WHERE department_id = @DepartmentId";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@DepartmentId", DepartmentId));
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
        public static Department GetDepartment(int id)
        {
            Department department = null;

            request = "SELECT department_id, department_name FROM department WHERE department_id = @DepartmentId";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("DepartmentId", id));
            connection.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                department = new Department()
                {
                    DepartmentId = reader.GetInt32(0),
                    DepartmentName = reader.GetString(1),
                };
            }
            reader.Close();
            command.Dispose();
            connection.Close();

            return department;
        }

        //Get the full sites list
        public static List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            request = "SELECT department_id, department_name FROM department ORDER BY department_name";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            connection.Open();
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Department department = new Department()
                {
                    DepartmentId = reader.GetInt32(0),
                    departmentName = reader.GetString(1),
                };
                departments.Add(department);
            }

            reader.Close();
            command.Dispose();
            connection.Close();
            return departments;
        }
    }
}
