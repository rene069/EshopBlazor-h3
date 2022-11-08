using Datalayer.Models;
using Eshop.CustomValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.Interface;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Eshop.Areas.Admin.Pages
{
    public class EditProductModel : PageModel
    {

        private readonly IProductService _productService;
        private readonly ICreateService _createService;
        private IHostingEnvironment _environment;

        public EditProductModel(IProductService productService, ICreateService createService, IHostingEnvironment environment)
        {
            _productService = productService;
            _createService = createService;
            _environment = environment;
        }
        [BindProperty]
        public Produkt Produkt { get; set; }
        public SelectList BrandList { get; set; }
        public SelectList TypeList { get; set; }
        [BindProperty]
        public string? BrandName { get; set; }
        [BindProperty]
        public string? TypeName { get; set; }
        [BindProperty,CustomFileExtention]
        public IFormFile formFile { get; set; }

        public IActionResult OnGet(int id)
        {
            int? roleID = HttpContext.Session.GetInt32("_RoleId");
            if (roleID != 1)
                return RedirectToPage("/index", new { area = "" });

            Produkt = _productService.GetProduktById(id);
            CreateSelectLists();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {


            if (!ModelState.IsValid)
            {
                OnGet(Produkt.ProduktId);
                return Page();
            }
            var file = Path.Combine(_environment.ContentRootPath, "wwwroot", "images", formFile.FileName);
            using (var filestream = new FileStream(file, FileMode.Create))
            {
                await formFile.CopyToAsync(filestream);
                Produkt.ImageURL = @"Images\" + formFile.FileName;

            }
            Produkt.BrandId = _productService.GetBrandByName(BrandName).BrandId;
            Produkt.TypesId = _productService.GetTypeByName(TypeName).TypesId;
            _createService.UpdateEntryGeneric(Produkt);

            return RedirectToPage("/index");
        }
        private void CreateSelectLists()
        {
            var brandQuery = from b in _productService.GetAllProducts()
                             orderby b.Brand.BrandId
                             select b.Brand.BrandName;
            var typeQuery = from t in _productService.GetAllProducts()
                            orderby t.Type.TypesId
                            select t.Type.TypeName;

            TypeList = new SelectList(typeQuery.Distinct().ToList());
            BrandList = new SelectList(brandQuery.Distinct().ToList());
        }
    }
}
