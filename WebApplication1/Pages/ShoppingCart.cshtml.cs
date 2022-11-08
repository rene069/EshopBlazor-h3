using Datalayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Eshop.Pages
{
    public class ShoppingCartModel : PageModel
    {
        public readonly ICartService _CartService;
        public readonly IUserService _UserService;
        public readonly IMailService _MailService;
        public readonly ICreateService _CreateService;
        public ShoppingCartModel(ICartService cartService, IUserService UserService, IMailService mailService,ICreateService createService)
        {
            _CartService = cartService;
            _UserService = UserService;
            _MailService = mailService;
            _CreateService = createService;
        }
        [BindProperty]
        public List<UserProdukt> UserProdukts { get; set; }
        [BindProperty]
        public List<UserProdukt> NewUserProdukts { get; set; }

        [BindProperty]
        public User User { get; set; }


        public void OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("_UserId");
            if (userId != null)
            {
                UserProdukts = _CartService.GetShoppingCartByUser(userId.Value);
                User = _UserService.GetUserById(userId.Value);
            }
        }

        public IActionResult OnPostUpdate()
        {
            int? userId = HttpContext.Session.GetInt32("_UserId");
            foreach (var item in NewUserProdukts)
            {
                item.UserId = userId.Value;
                if (item.Quantity == 0)
                {
                    _CreateService.DeleteEntryGeneric(item);
                }
                else
                {
                    _CreateService.UpdateEntryGeneric(item);
                }
                
            }
            
            return RedirectToPage("/ShoppingCart");
        }

        public IActionResult OnPostCheckOut()
        {
            int? userId = HttpContext.Session.GetInt32("_UserId");
            if (_CartService.GetShoppingCartByUser(userId.Value).Any())
            {
                _MailService.SendCheckOutMail(_UserService.GetUserById(userId.Value));
                foreach (var item in _CartService.GetShoppingCartByUser(userId.Value))
                {
                    _CreateService.DeleteEntryGeneric(_CartService.GetUserProduktById(item.UserId, item.ProduktId));
                    return RedirectToPage("/index");
                }
            }
            else
            {
                ModelState.AddModelError("", "there are no items in your Cart");

            }

            return Page();
           
            
          
        }
    }
}
