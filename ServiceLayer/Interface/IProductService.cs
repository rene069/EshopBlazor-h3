using Datalayer.Models;
using ServiceLayer.DTO;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IProductService
    {


        public Task<Produkt>UpdateProduktAsync(Produkt produkt);

        public List<Produkt> SortProdukts(SortFilterOptions sortFilterOptions, List<Produkt> produkts);
        public List<Produkt> Paging(int page);

        public List<Produkt> FindProdukts(string searchString);
        List<ProduktDto> ProduktsToProduktListDto();
        ProduktDto ProduktToDTO(int id);
        public int GetCount();

        public bool DoesProduktExist(int id);
        public Produkt GetProduktById(int ID);
        public Brand GetBrandByName(string name);
        public Types GetTypeByName(string name);

        public List<Produkt> GetAllProducts();
        public List<Brand> GetAllBrands();
        public List<Types> GetAllTypes();

    }
}
