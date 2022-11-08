using Datalayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using ServiceLayer.Repository;
using System.Collections.ObjectModel;

namespace EshopAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public readonly ICreateService _createService;
        public UserController(IUserService userService, ICreateService createService)
        {
            _userService = userService;
            _createService = createService;
        }

        /// <summary>
        /// Creates User [dont use this way]
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateUser")]
        public ActionResult CreateUser(string username, string password, string email, string address, int zipCode)
        {
            User user = new User()
            {
                UserName = username,
                Password = password,
                Email = email,
                Address = address,
                ZipCode = zipCode,
                RoleId = 3
            };
            try
            {
                if (_userService.UserExists(user.UserName))
                    return StatusCode(StatusCodes.Status500InternalServerError, "User Allready Exists");

                _createService.AddNewEntryGeneric(user);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error Adding User");
            }
        }
    }
}
