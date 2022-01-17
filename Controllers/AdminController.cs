using annuaire.Models;
using annuaire.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace annuaire.Controllers
{
    public class AdminController : Controller
    {
        private ILogin _loginService;

        //Add login interface in the constructor
        public AdminController(ILogin login)
        {
            _loginService = login;
        }

        public IActionResult Index()
        {
            if (!_loginService.IsLogin())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //List of sites
        public IActionResult SiteEdit(string message)
        {
            if (!_loginService.IsLogin())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = message;

            return View(Site.GetSites());
        }

        //Delete a site
        public IActionResult SiteDelete(int id)
        {

            if (!_loginService.IsLogin())
            {
                return RedirectToAction("Index", "Home");
            }

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

        //Site form
        public IActionResult SiteForm(int id)
        {
            if (!_loginService.IsLogin())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Action = id == 0 ? "CreateSite" : "UpdateSite";

            return View(Site.GetSite(id));
        }

        //Update site
        public IActionResult UpdateSite(Site site)
        {
            if (!_loginService.IsLogin())
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
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

            return View("SiteForm", site);

        }

        //Create site
        public IActionResult CreateSite([Bind("SiteName, SiteType")] Site site)
        {
            if (!_loginService.IsLogin())
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
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

            return View("SiteForm", site);
 
        }

        //List of departments
        public IActionResult DepartmentEdit(string message)
        {
            if (!_loginService.IsLogin())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = message;

            return View(Department.GetDepartments());
        }

        //Delete department
        public IActionResult DepartmentDelete(int id)
        {
            if (!_loginService.IsLogin())
            {
                return RedirectToAction("Index", "Home");
            }

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

        //Department form
        public IActionResult DepartmentForm(int id)
        {
            if (!_loginService.IsLogin())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Action = id == 0 ? "CreateDepartment" : "UpdateDepartment";

            return View(Department.GetDepartment(id));
        }

        //Update department
        public IActionResult UpdateDepartment(Department department)
        {
            if (!_loginService.IsLogin())
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
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

            return View("DepartmentForm", department);

        }

        //Create department
        public IActionResult CreateDepartment([Bind("DepartmentName")] Department department)
        {
            if (!_loginService.IsLogin())
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
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

            return View("DepartmentForm", department);

        }

        //List of employees
        public IActionResult EmployeeEdit(string message)
        {
            if (!_loginService.IsLogin())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = message;

            return View(Employee.GetEmployees());
        }

        //Delete employee
        public IActionResult EmployeeDelete(int id)
        {
            if (!_loginService.IsLogin())
            {
                return RedirectToAction("Index", "Home");
            }

            string msg = null;

            Employee employee = Employee.GetEmployee(id);

            if(employee != null)
            {
                if (!employee.Delete()) { msg = "Erreur rencontrée lors de la suppression"; }
            }

            return RedirectToAction("EmployeeEdit", "Admin", new { message = msg });
        }

        //Employee form
        public IActionResult EmployeeForm(int id)
        {
            if (!_loginService.IsLogin())
            {
                return RedirectToAction("Index", "Home");
            }

            //select Create or Update form
            ViewBag.Action = id == 0 ? "CreateEmployee" : "UpdateEmployee";
            ViewBag.Sites = Site.GetSites();
            ViewBag.Departments = Department.GetDepartments();

            return View(Employee.GetEmployee(id));
        }

        //Update employee
        public IActionResult UpdateEmployee(Employee employee, Site site, Department department)
        {
            if (!_loginService.IsLogin())
            {
                return RedirectToAction("Index", "Home");
            }

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

        //Create employee
        public IActionResult CreateEmployee([Bind("FirstName, LastName, LandlinePhone, MobilePhone, Email")] Employee employee, [Bind("SiteId")] Site site, [Bind("DepartmentId")] Department department)
        {
            if (!_loginService.IsLogin())
            {
                return RedirectToAction("Index", "Home");
            }

            employee.Site = Site.GetSite(site.SiteId);
            employee.Department = Department.GetDepartment(department.DepartmentId);


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
