using annuaire.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace annuaire.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SiteEdit(string message)
        {
            ViewBag.Message = message;

            return View(Site.GetSites());
        }

        //Delete a site
        public IActionResult SiteDelete(int id)
        {
            string msg = null;

            Site site = Site.GetSite(id);

            if (site != null)
            {
                if (site.DeleteVerification())
                {
                    site.Delete();
                } else
                {
                    msg = "Impossible de supprimer le site";
                }
            }

            return RedirectToAction("SiteEdit", "Admin", new { message = msg });
        }

        public IActionResult SiteForm(int id)
        {
            ViewBag.Action = id == 0 ? "CreateSite" : "UpdateSite";

            return View(Site.GetSite(id));
        }

        public IActionResult UpdateSite(Site site)
        {
            if (site.SaveModifications())
            {
                return RedirectToAction("SiteEdit", "Admin", new { message = "Modifications effectuées" });
            }
            else
            {
                return RedirectToAction("SiteEdit", "Admin", new { message = "Problème lors de l'enregistrement" });
            }
        }

        public IActionResult CreateSite(Site site)
        {
            if (site.Save())
            {
                return RedirectToAction("SiteEdit", "Admin", new { message = "Nouveau site enregistré" });
            }
            else
            {
                return RedirectToAction("SiteEdit", "Admin", new { message = "Problème lors de l'enregistrement" });
            }
        }

        public IActionResult DepartmentEdit(string message)
        {
            ViewBag.Message = message;

            return View(Department.GetDepartments());
        }

        public IActionResult DepartmentDelete(int id)
        {
            string msg = null;

            Department department = Department.GetDepartment(id);

            if (department != null)
            {
                if (department.DeleteVerification())
                {
                    department.Delete();
                }
                else
                {
                    msg = "Impossible de supprimer le service";
                }
            }

            return RedirectToAction("DepartmentEdit", "Admin", new { message = msg });
        }

        public IActionResult DepartmentForm(int id)
        {
            ViewBag.Action = id == 0 ? "CreateDepartment" : "UpdateDepartment";

            return View(Department.GetDepartment(id));
        }

        public IActionResult UpdateDepartment(Department department)
        {
            if (department.SaveModifications())
            {
                return RedirectToAction("DepartmentEdit", "Admin", new { message = "Modifications effectuées" });
            }
            else
            {
                return RedirectToAction("DepartmentEdit", "Admin", new { message = "Problème lors de l'enregistrement" });
            }
        }

        public IActionResult CreateDepartment(Department department)
        {
            if (department.Save())
            {
                return RedirectToAction("DepartmentEdit", "Admin", new { message = "Nouveau service enregistré" });
            }
            else
            {
                return RedirectToAction("DepartmentEdit", "Admin", new { message = "Problème lors de l'enregistrement" });
            }
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
