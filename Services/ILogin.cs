using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace annuaire.Services
{
    public interface ILogin
    {
        void SaveAccessToSession(string username, string password);
        bool IsLogin();
        string getNameFromSession();
    }
}
