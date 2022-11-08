using Datalayer;
using Datalayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.Interface;
using ServiceLayer.QueryObjects;
using ServiceLayer.Repository;
using ServiceLayer.Services;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {

        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICreateService _createService;
        public IndexModel(IProductService productService, ICartService cartService, ICreateService createService)
        {
            _productService = productService;
            _cartService = cartService;
            _createService = createService;
        }

        [BindProperty]
        public List<Produkt> Produkts { get; set; } = new List<Produkt>();
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public SelectList BrandList { get; set; }
        public SelectList TypeList { get; set; }

        [BindProperty(SupportsGet = true)]
        public SortFilterOptions OrderOptions { get; set; } = new SortFilterOptions();
        [BindProperty(SupportsGet = true)]
        public string? BrandName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? TypeName { get; set; }

        #region Paging
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; } = 0;
        public int PageSize { get; set; } = 2;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        #endregion

        //TODO: make Searching work with brand and type search
        public void OnGet()
        {
            var brandQuery = from b in _productService.GetAllProducts()
                             orderby b.Brand.BrandId
                             select b.Brand.BrandName;
            var typeQuery = from t in _productService.GetAllProducts()
                            orderby t.Type.TypesId
                            select t.Type.TypeName;


            if (!string.IsNullOrEmpty(SearchString))
                Produkts = _productService.GetAllProducts().Where(x => x.ProduktName.Contains(SearchString)).ToList();

            if (!string.IsNullOrEmpty(BrandName))
                Produkts = _productService.GetAllProducts().Where(x => x.Brand.BrandName == BrandName).ToList();

            if (!string.IsNullOrEmpty(TypeName))
                Produkts = _productService.GetAllProducts().Where(x => x.Type.TypeName == TypeName).ToList();

            if (Produkts.Count() == 0)
            {
                Produkts = _productService.Paging(CurrentPage);
                Count = _productService.GetAllProducts().Count();
            }
            
            
            Produkts = _productService.SortProdukts(OrderOptions, Produkts);
            TypeList = new SelectList(typeQuery.Distinct().ToList());
            BrandList = new SelectList(brandQuery.Distinct().ToList());


        }

        public List<Produkt> GetProdukts()
        {
            return Produkts;
        }

        public IActionResult OnPostAddToCart(int productId)
        {
            int? userId = HttpContext.Session.GetInt32("_UserId");
            if (userId == null)
            {
                ModelState.AddModelError("", "Please Login to add products to you cart");
                OnGet();
                return Page();
            }
            Produkt produkt = _productService.GetProduktById(productId);
            if (_cartService.DoesProductExistInCart(productId))
            {
                UserProdukt userProdukt = _cartService.GetUserProduktById(userId.Value, productId);
                userProdukt.Quantity += 1;
                _createService.UpdateEntryGeneric(userProdukt);
                
            }
            else
            {
                UserProdukt userProdukt = new()
                {
                    Quantity = 1,
                    UserId = userId.Value,
                    ProduktId = productId

                };
                _createService.AddNewEntryGeneric(userProdukt);
            }
            
            return RedirectToPage("/ShoppingCart");
        }
    }
}