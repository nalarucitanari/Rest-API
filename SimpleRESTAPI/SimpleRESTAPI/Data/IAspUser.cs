using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTAPI.Models;

namespace SimpleRESTAPI.Data
{
    public interface IAspUser
    {
        IEnumerable<AspUser> GetAllUsers();
        AspUser RegisterUser(AspUser user);
        AspUser GetUserByUsername(string username);
        AspUser UpdateUser(AspUser user);
        void DeleteUser(string username);
        bool Login(string username, string password);

    }
}