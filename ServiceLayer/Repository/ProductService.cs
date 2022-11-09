using Datalayer;
using Datalayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;
using ServiceLayer.QueryObjects;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.QueryObjects;
using ServiceLayer.DTO;
using Microsoft.AspNetCore.Mvc.Internal;

namespace ServiceLayer.Repository
{

    public class ProductService : IProductService
    {
        public readonly EShopContext _eShopContext;

        public ProductService(EShopContext eShopContext)
        {
            _eShopContext = eShopContext;
        }

        public List<Produkt> SortProdukts(SortFilterOptions sortFilterOptions, List<Produkt> produkts)
        {
            switch (sortFilterOptions.OrderByOptions)
            {
                case OrderByOptions.SimpleOrder:
                    return produkts.OrderProduktsBy(sortFilterOptions.OrderByOptions).ToList();
                case OrderByOptions.ByNameDesc:
                    return produkts.OrderProduktsBy(sortFilterOptions.OrderByOptions).ToList();
                case OrderByOptions.ByPrice:
                    return produkts.OrderProduktsBy(sortFilterOptions.OrderByOptions).ToList();
                case OrderByOptions.ByBrand:
                    return produkts.OrderProduktsBy(sortFilterOptions.OrderByOptions).ToList();
                default:
                    return null;

            }
        }

        public async Task<Produkt> UpdateProduktAsync(Produkt produkt)
        {
            var result = await _eShopContext.Produkts.FirstOrDefaultAsync(e => e.ProduktId == produkt.ProduktId);

            if (result != null)
            {
                result.ProduktName = produkt.ProduktName;
                result.Price = produkt.Price;
                result.Description = produkt.Description;
                result.BrandId = produkt.BrandId;
                result.TypesId = produkt.TypesId;
                result.ImageURL = produkt.ImageURL;
                result.IsSoftDeleted = produkt.IsSoftDeleted;

                await _eShopContext.SaveChangesAsync();

                return result;
            }
            return null;
        }

        public ProduktDto ProduktToDTO(int id) => _eShopContext.Produkts.Where(x => x.ProduktId == id).Include(x => x.Brand).Include(x => x.Type).FirstOrDefault().MapProduktToDto();

        public List<ProduktDto> ProduktsToProduktListDto() => _eShopContext.Produkts.Include(x => x.Brand).Include(x => x.Type).MapProduktsToDto().ToList();
        public List<Brand> GetAllBrands() => _eShopContext.Brands.ToList();
        public List<Types> GetAllTypes() => _eShopContext.Types.ToList();

        public List<Produkt> GetAllProducts() => _eShopContext.Produkts.Where(x => x.IsSoftDeleted == false)
            //.Include(x => x.Type)
            //.Include(x => x.Brand)
            .OrderBy(x => x.ProduktId).ToList();
        public List<Produkt> FindProdukts(string searchString) => _eShopContext.Produkts.Where(x => x.ProduktName.Contains(searchString))
            .Include(x => x.Brand)
            .Include(x => x.Type).ToList();

       
        public List<Produkt> Paging(int page) => _eShopContext.Produkts
            .Include(x => x.Brand)
            .Include(x => x.Type)
            .Paging(page, 2).ToList();

        public bool DoesProduktExist(int id) => _eShopContext.Produkts.Where(x => x.ProduktId == id).Any();
        public Brand GetBrandByName(string name) => _eShopContext.Brands.Where(x => x.BrandName == name).SingleOrDefault();
        public Types GetTypeByName(string name) => _eShopContext.Types.Where(x => x.TypeName == name).SingleOrDefault();
        public Produkt GetProduktById(int ID) => _eShopContext.Produkts.Where(x => x.ProduktId == ID).SingleOrDefault();
        public int GetCount() => GetAllProducts().Count();
    }
}
