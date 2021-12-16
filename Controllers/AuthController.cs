using annuaire.Models;
using annuaire.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace annuaire.Controllers
{
    public class AuthController : Controller
    {

        private ILogin _loginService;

        public AuthController(ILogin login)
        {
            _loginService = login;

        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LoginAccess(UserLogin user)
        {
            if (UserLogin.CheckAuthentification(user))
            {
                _loginService.SaveAccessToSession(user.Username, user.Password);
                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
