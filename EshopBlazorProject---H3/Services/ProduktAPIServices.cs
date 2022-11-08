using EshopBlazor.Models;
using System.Net.Http.Json;

namespace EshopBlazor.Services
{
    public class ProduktAPIServices : IProduktServices
    {
        private readonly HttpClient _httpClient;
        public ProduktAPIServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateProduktAsync(Produkt produktDTO)
        {
            var response = await _httpClient.PostAsJsonAsync<Produkt>("/produkts/CreateProdukt", produktDTO);

            response.EnsureSuccessStatusCode();

           
        }

        public async Task<List<Produkt>> GetProduktsAsync()
        {
            var items = await _httpClient.GetFromJsonAsync<List<Produkt>>("/Produkts/GetProdukts");

            return items.AsQueryable().ToList();

        }

        public async Task<List<BrandDTO>> GetBrandAsync()
        {
            var items = await _httpClient.GetFromJsonAsync<List<BrandDTO>>("/Brand/GetBrands");

            return items.AsQueryable().ToList();
        }

        public async Task<List<TypesDTO>> GetTypesAsync()
        {
            var items = await _httpClient.GetFromJsonAsync<List<TypesDTO>>("/Types/GetAllTypes");

            return items.AsQueryable().ToList();
        }
    }


    public static class ProduktMapper
    {

    }
}
