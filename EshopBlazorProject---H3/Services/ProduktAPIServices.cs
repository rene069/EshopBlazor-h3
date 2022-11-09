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

        public async Task CreateProduktAsync(ProduktDTO produkt)
        {
            var response = await _httpClient.PostAsJsonAsync<ProduktDTO>("/produkts/CreateProdukt", produkt);

            response.EnsureSuccessStatusCode();


        }
        
        public async Task<ProduktShown> GetProduktShownAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ProduktShown>($"/produkts/GetProduktDtoById?id={id}");
          
            return response;
        }
        public async Task EditProduktAsync(ProduktDTO produkt)
        {
            var response = await _httpClient.PutAsJsonAsync<ProduktDTO>("/Produkts/UpdateProdukt", produkt);

            response.EnsureSuccessStatusCode();
        }

        public async Task<List<ProduktDTO>> GetProduktsAsync()
        {
            var items = await _httpClient.GetFromJsonAsync<List<ProduktDTO>>("/Produkts/GetProdukts");

            return items.AsQueryable().ToList();

        }

        public async Task<ProduktDTO> GetProduktByIdAsync(int id)
        {
            var items = await _httpClient.GetFromJsonAsync<ProduktDTO>($"/Produkts/GetProduktById?id={id}");



            return items;
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

        public async Task DeleteProdukt(int id)
        {

            var item = await _httpClient.DeleteAsync($"/produkts/SoftDeleteProdukt?id={id}");

            item.EnsureSuccessStatusCode();
        }
    }


    public static class ProduktMapper
    {

    }
}
