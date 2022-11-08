using Datalayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.Interface;
using System.ComponentModel.DataAnnotations;

namespace Eshop.Pages
{


    public class LoginModel : PageModel
    {

        public const string SessionKeyId = "_UserId";
        public const string SessionRoleId = "_RoleId";

        private readonly IUserService _userService;
        public LoginModel(IUserService userService)
        {
            _userService = userService;

        }

        [BindProperty, Required]
        public string Username { get; set; }
        [BindProperty, Required]
        public string Password { get; set; }


        public IActionResult OnGet(bool logout)
        {
            if (logout)
            {
                HttpContext.Session.Remove(SessionKeyId);
                HttpContext.Session.Remove(SessionRoleId);
                return RedirectToPage("/index");
            }
            return Page();
        }

        public IActionResult OnPostLogin()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = _userService.GetUserForLogin(Username, Password);

            if (user == null)
            {
                ModelState.AddModelError("User", "Wrong Username or Password");
                return Page();
            }
            HttpContext.Session.SetInt32(SessionKeyId, user.UserId);
            HttpContext.Session.SetInt32(SessionRoleId, user.RoleId);

            return RedirectToPage("/index");
        }


    }
}
