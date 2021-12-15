using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace annuaire.Controllers
{
    public class Auth : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
