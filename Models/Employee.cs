using annuaire.Tools;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace annuaire.Models
{
    public class Employee
    {
        private int employeeId;
        private string firstName;
        private string lastName;
        private string landlinePhone;
        private string mobilePhone;
        private string email;
        private Site site;
        private Department department;
        private static string request;
        private static MySqlCommand command;
        private static MySqlConnection connection;
        private static MySqlDataReader reader;

        public int EmployeeId { get => employeeId; set => employeeId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string LandlinePhone { get => landlinePhone; set => landlinePhone = value; }
        public string MobilePhone { get => mobilePhone; set => mobilePhone = value; }
        public string Email { get => email; set => email = value; }
        public Site Site { get => site; set => site = value; }
        public Department Department { get => department; set => department = value; }

        //Save a new employee
        public bool Save()
        {
            request = "INSERT INTO employee (firstname, lastname, landline_phone, mobile_phone, e_mail, site_id, department_id) values (@firstName, @lastName, @landlinePhone, @mobilePhone, @email, @siteId, @departmentId); SELECT LAST_INSERT_ID()";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@firstName", FirstName));
            command.Parameters.Add(new MySqlParameter("@lastName", LastName));
            command.Parameters.Add(new MySqlParameter("@landlinePhone", LandlinePhone));
            command.Parameters.Add(new MySqlParameter("@mobilePhone", MobilePhone));
            command.Parameters.Add(new MySqlParameter("@email", Email));
            command.Parameters.Add(new MySqlParameter("@siteId", Site.SiteId));
            command.Parameters.Add(new MySqlParameter("@departmentId", Department.DepartmentId));
            connection.Open();
            EmployeeId = Convert.ToInt32(command.ExecuteScalar());
            command.Dispose();
            connection.Close();
            return EmployeeId > 0;
        }

        //Update the employee in database
        public bool SaveModifications()
        {
            request = "UPDATE employee  SET firstname = @firstName, lastname = @lastName, landline_phone = @landlinePhone, mobile_phone = @mobilePhone, e_mail = @email, site_id = @siteId, department_id = @departmentId WHERE employee_id = @EmployeeId";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@firstName", FirstName));
            command.Parameters.Add(new MySqlParameter("@lastName", LastName));
            command.Parameters.Add(new MySqlParameter("@landlinePhone", LandlinePhone));
            command.Parameters.Add(new MySqlParameter("@mobilePhone", MobilePhone));
            command.Parameters.Add(new MySqlParameter("@email", Email));
            command.Parameters.Add(new MySqlParameter("@siteId", Site.SiteId));
            command.Parameters.Add(new MySqlParameter("@departmentId", Department.DepartmentId));
            command.Parameters.Add(new MySqlParameter("@EmployeeId", EmployeeId));

            connection.Open();
            int response = command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            return response == 1;
        }

        //Delete an employee from the database
        public bool Delete()
        {
            request = "DELETE FROM employee WHERE employee_id=@EmployeeId";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@EmployeeId", EmployeeId));
            connection.Open();
            int nb = command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            return nb == 1;
        }

        public static Employee GetEmployee(int id)
        {
            Employee employee = null;

            request = "SELECT employee_id, firstname, lastname, landline_phone, mobile_phone, e_mail, site_id, department_id FROM employee WHERE employee_id = @EmployeeId";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("EmployeeId", id));
            connection.Open();
            reader = command.ExecuteReader();

            if (reader.Read())
            {
                employee = new Employee()
                {
                    EmployeeId = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    LandlinePhone = reader.GetString(3),
                    MobilePhone = reader.GetString(4),
                    Email = reader.GetString(5),
                    Department = Department.GetDepartment(reader.GetInt32(7)),
                    Site = Site.GetSite(reader.GetInt32(6))
                };
            }

            reader.Close();
            command.Dispose();
            connection.Close();

            return employee;
        }

        public static List<Employee> GetEmployees(string where = "")
        {
            List<Employee> employees = new List<Employee>();
            request = "SELECT employee_id, firstname, lastname, landline_phone, mobile_phone, e_mail, site_id, department_id FROM employee";
            if (where != ""){ request += " WHERE " + where; }
            request += " " + "ORDER BY lastname";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            connection.Open();
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Employee employee = new Employee()
                {
                    EmployeeId = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    LandlinePhone = reader.GetString(3),
                    MobilePhone = reader.GetString(4),
                    Email = reader.GetString(5),
                    Department = Department.GetDepartment(reader.GetInt32(7)),
                    Site = Site.GetSite(reader.GetInt32(6))
                };
                employees.Add(employee);
            }

            reader.Close();
            command.Dispose();
            connection.Close();
            return employees;
        }

    }
}
