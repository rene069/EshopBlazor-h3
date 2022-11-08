using EshopBlazor.Models;

namespace EshopBlazor.Services
{
    public interface IProduktServices
    {
        Task CreateProduktAsync(Produkt produktDTO);
        Task<List<Produkt>> GetProduktsAsync();
        Task<List<BrandDTO>> GetBrandAsync();
        Task<List<TypesDTO>> GetTypesAsync();

    }
}
