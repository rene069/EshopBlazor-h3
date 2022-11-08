using Datalayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.Interface;
using System.ComponentModel.DataAnnotations;

namespace Eshop.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ICreateService _createService;
        private readonly IUserService _userService;

        
        public RegisterModel(ICreateService createService, IUserService userService)
        {
            _createService = createService;
            _userService = userService;
        }

        public void OnGet()
        {

        }

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Address { get; set; }
        [BindProperty]
        public int Zip { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty, Compare("Password", ErrorMessage = "Passwords need to match")]
        public string RepeatPass { get; set; }
        public IActionResult OnPost()
        {
                       
            if (_userService.UserExists(Username))            
                ModelState.AddModelError("Username", "Username is allready in use");
            
            if (!ModelState.IsValid)
                return Page();
            User user = new User() { UserName = Username, Password = Password,ZipCode = Zip,Address = Address, Email = Email, RoleId = 3 };
            _createService.AddNewEntryGeneric(user);

            return RedirectToPage("/Index");
        }
    }
}
