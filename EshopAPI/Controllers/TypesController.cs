using Datalayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace EshopAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly ICreateService _createService;

        public TypesController(IProductService productService, ICreateService createService)
        {
            _productService = productService;
            _createService = createService;
        }


        [HttpGet]
        [Route("GetAllTypes")]
        public List<Types> GetTypes()
        {
            return _productService.GetAllTypes();
        }

    }
}
