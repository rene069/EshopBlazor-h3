using EshopBlazor.Models;
using System.Threading.Tasks;

namespace EshopBlazor.Services
{
    public interface IProduktServices
    {
        Task CreateProduktAsync(Produkt produktDTO);
        Task<Produkt> GetProduktByIdAsync(int id);
        Task<List<Produkt>> GetProduktsAsync();
        Task<List<BrandDTO>> GetBrandAsync();
        Task<List<TypesDTO>> GetTypesAsync();

    }
}
