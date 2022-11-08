using Datalayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.DTO;
using ServiceLayer.Interface;
using ServiceLayer.QueryObjects;
using ServiceLayer.Services;

namespace EshopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProduktsController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly ICreateService _createService;

        public ProduktsController(IProductService productService, ICreateService createService)
        {
            _productService = productService;
            _createService = createService;
        }

        /// <summary>
        /// Get all Produkts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProdukts")]
        public List<Produkt> GetProdukts()
        {
            return _productService.GetAllProducts();
        }

        /// <summary>
        /// Sort produkts
        /// </summary>
        /// <param name="orderByOptions">Produkt name = 0, Produkt name Desc = 1, By Price = 2, by brand = 3</param>
        /// <returns></returns>
        [HttpGet]
        [Route("SortProdukt")]
        public ActionResult GetProduktsSorted(OrderByOptions orderByOptions)
        {
            SortFilterOptions sortOptions = new();
            sortOptions.OrderByOptions = orderByOptions;
            List<Produkt> produkts = _productService.GetAllProducts();

            try
            {
                return Ok(_productService.SortProdukts(sortOptions, produkts));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error can't get Data");
            }

        }

        /// <summary>
        /// Gets a Single Produkt by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProduktById")]
        public ActionResult GetProduktById(int id)
        {
            try
            {
                return Ok(_productService.GetProduktById(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error can't get Data or no data with that Id");
            }
        }

        /// <summary>
        /// Gets a Single Produkt by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProduktById")]
        public ActionResult GetProduktDTOById(int id)
        {
            try
            {                
                var produkt = _productService.GetProduktById(id);
                return Ok(_productService.GetProduktById(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error can't get Data or no data with that Id");
            }
        }

        /// <summary>
        /// Create Produkt
        /// </summary>
        /// <param name="produkt">, write as Json</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateProdukt")]
        public ActionResult CreateProdukt(Produkt produkt)
        {
            try
            {
                _createService.AddNewEntryGeneric(produkt);

                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                   "Error Adding data");
            }
        }


        /// <summary>
        /// Soft Deletes produkts
        /// </summary>
        /// <param name="id">Id for the Produkt to be SoftDeleted</param>
        /// <returns></returns>
        [HttpPut]
        [Route("SoftDeleteProdukt")]
        public ActionResult SoftDeleteProdukt(int id)
        {
            try
            {
                Produkt ToDeleteProdukt = _productService.GetProduktById(id);

                if (ToDeleteProdukt == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "Can't Find Produkt");

                ToDeleteProdukt.IsSoftDeleted = true;

                _createService.UpdateEntryGeneric(ToDeleteProdukt);

                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                                  "Error Updating data");
            }
        }

        /// <summary>
        /// Deletes Produkts Completely
        /// </summary>
        /// <param name="id">Id for the Produkt to be deleted</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteProdukt")]
        public ActionResult DeleteProdukt(int id)
        {
            try
            {
                Produkt ToDeleteProdukt = _productService.GetProduktById(id);

                if (ToDeleteProdukt == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "Can't Find Produkt");


                _createService.DeleteEntryGeneric(ToDeleteProdukt);

                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                                  "Error Deleting data");
            }
        }

        /// <summary>
        /// Update Produkts
        /// </summary>
        /// <param name="produkt">Sends Produkt to update, write as Json</param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateProdukt")]
        public ActionResult UpdateProdukt(Produkt produkt)
        {
            try
            {
                if (!_productService.DoesProduktExist(produkt.ProduktId))
                    return StatusCode(StatusCodes.Status500InternalServerError, "Can't Find Produkt");

                _createService.UpdateEntryGeneric(produkt);

                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                   "Error updating data");
            }
        }
    }
}