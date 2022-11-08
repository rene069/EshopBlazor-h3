using Datalayer;
using Datalayer.Models;
using Microsoft.AspNetCore.Http;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository
{
    public class UserService : IUserService
    {

        
        readonly EShopContext _eShopContext;
        public UserService(EShopContext eShopContext)
        {
            _eShopContext = eShopContext;
        }

        public bool UserExists(string username) => _eShopContext.Users.Where(x => x.UserName == username).Any();
        public User GetUserById(int id) => _eShopContext.Users.Where(x => x.UserId == id).FirstOrDefault();
        public User GetUserForLogin(string username, string password) => _eShopContext.Users.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();
    }
}
