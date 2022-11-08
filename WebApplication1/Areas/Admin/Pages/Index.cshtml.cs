using Datalayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.DTO;
using ServiceLayer.Interface;
using ServiceLayer.Repository;

namespace Eshop.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {

        private readonly IProductService _productService;
        private readonly ICreateService _createService;

        public IndexModel(IProductService productService, ICreateService createService)
        {
            _productService = productService;
            _createService = createService;
        }
        [BindProperty]
        public List<ProduktDto>? Produkts { get; set; }


        public IActionResult OnGet()
        {
            int? roleID = HttpContext.Session.GetInt32("_RoleId");
            if (roleID != 1)
                return RedirectToPage("/index", new { area = "" });

            Produkts = _productService.ProduktsToProduktListDto();
            return Page();
        }
        public IActionResult OnPostSoftDelete(int id)
        {
            Produkt produkt = _productService.GetProduktById(id);
            produkt.IsSoftDeleted = !produkt.IsSoftDeleted;

            _createService.UpdateEntryGeneric(_productService.GetProduktById(id));
            return RedirectToPage("");
        }
        public IActionResult OnPostDelete(int id)
        {
            _createService.DeleteEntryGeneric(_productService.GetProduktById(id));
            return RedirectToPage("");
        }
    }
}
