using Datalayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace EshopAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly ICreateService _createService;

        public BrandController(IProductService productService, ICreateService createService)
        {
            _productService = productService;
            _createService = createService;
        }

        /// <summary>
        /// Get all Brand
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBrands")]
        public List<Brand> GetBrands()
        {
            return _productService.GetAllBrands();
        }
    }
}
