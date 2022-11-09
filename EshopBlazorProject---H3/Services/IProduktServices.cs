using EshopBlazor.Models;
using System.Threading.Tasks;

namespace EshopBlazor.Services
{
    public interface IProduktServices
    {
        Task CreateProduktAsync(ProduktDTO produkt);
        Task EditProduktAsync(ProduktDTO produkt);
        Task<ProduktShown> GetProduktShownAsync(int id);
        Task DeleteProdukt(int id);
        Task<ProduktDTO> GetProduktByIdAsync(int id);
        Task<List<ProduktDTO>> GetProduktsAsync();
        Task<List<BrandDTO>> GetBrandAsync();
        Task<List<TypesDTO>> GetTypesAsync();

    }
}
