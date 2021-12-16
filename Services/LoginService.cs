using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace annuaire.Services
{
    public class LoginService : ILogin
    {
        private IHttpContextAccessor _httpContext;
        public LoginService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public void SaveAccessToSession(string user, string password)
        {
            _httpContext.HttpContext.Session.SetString("user", user);
            _httpContext.HttpContext.Session.SetString("password", password);
        }

        public bool IsLogin()
        {
            return _httpContext.HttpContext.Session.GetString("user") != null && _httpContext.HttpContext.Session.GetString("password") != null;
        }
        public string getNameFromSession()
        {
            return _httpContext.HttpContext.Session.GetString("user");
        }
    }
}
