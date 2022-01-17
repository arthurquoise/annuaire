using annuaire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace annuaire.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //Default home page
        public IActionResult Index()
        {
            ViewBag.Sites = Site.GetSites();
            ViewBag.Departments = Department.GetDepartments();

            return View();
        }

        //List of employee
        public IActionResult EmployeeList(string name = null, int departmentId = 0, int siteId = 0)
        {
            string where = "";

            if(name != null)
            {
                where += (where == "") ? $"lastname like '{name}%'" : $" AND lastname like '{name}%'";
            }

            if (departmentId != 0)
            {
                where += (where == "") ? $"department_id = {departmentId}" : $" AND department_id like {departmentId}";
            }

            if (siteId != 0)
            {
                where += (where == "") ? $"site_id = {siteId}" : $" AND site_id like {siteId}";
            }

            return View(Employee.GetEmployees(where));
        }

        //Employee detail
        public IActionResult EmployeeDetail(int id)
        {
            return View(Employee.GetEmployee(id));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
