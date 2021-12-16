using annuaire.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace annuaire.Controllers
{
    public class Admin : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SiteEdit()
        {
            return View(Site.GetSites());
        }

        public IActionResult DepartmentEdit()
        {
            return View(Department.GetDepartments());
        }

        public IActionResult EmployeeEdit(string message)
        {
            ViewBag.Message = message;

            return View(Employee.GetEmployees());
        }

        public IActionResult EmployeeDelete(int id)
        {
            string msg = null;

            Employee employee = Employee.GetEmployee(id);

            if(employee != null)
            {
                if (!employee.Delete()) { msg = "Erreur rencontrée lors de la suppression"; }
            }

            return RedirectToAction("EmployeeEdit", "Admin", new { message = msg });
        }

        public IActionResult EmployeeForm(int id)
        {
            ViewBag.Action = id == 0 ? "CreateEmployee" : "UpdateEmployee";
            ViewBag.Sites = Site.GetSites();
            ViewBag.Departments = Department.GetDepartments();

            return View(Employee.GetEmployee(id));
        }

        public IActionResult UpdateEmployee(Employee employee, Site site, Department department)
        {
            employee.Site = site;
            employee.Department = department;

            if (employee.SaveModifications())
            {
                return RedirectToAction("EmployeeEdit", "Admin", new { message = "Modifications effectuées" });
            }
            else
            {
                return RedirectToAction("EmployeeEdit", "Admin", new { message = "Problème lors de l'enregistrement" });
            }
        }

        public IActionResult CreateEmployee(Employee employee, Site site, Department department)
        {
            employee.Site = site;
            employee.Department = department;

            if (employee.Save())
            {
                return RedirectToAction("EmployeeEdit", "Admin", new { message = "Employée créé" });
            }
            else
            {
                return RedirectToAction("EmployeeEdit", "Admin", new { message = "Problème lors de la création" });
            }
        }

    }
}
