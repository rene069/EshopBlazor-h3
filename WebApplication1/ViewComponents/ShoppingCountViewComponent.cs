using Datalayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace Eshop.Viewcomponents
{
    public class ShoppingCountViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;
        public ShoppingCountViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var count = 0;
            int? Userid = HttpContext.Session.GetInt32("_UserId");
            if (Userid > 0)
            {
                foreach (var item in await _cartService.GetShoppingCartByUserAsync(Userid.Value))
                {
                    count += item.Quantity;
                }
               

            }
            return View(count);

            
        }
    }
}
