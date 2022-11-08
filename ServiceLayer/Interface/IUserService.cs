using Datalayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IUserService
    {
        User GetUserForLogin(string username, string password);

        bool UserExists(string username);

        User GetUserById(int id);


    }
}
