namespace EshopBlazor.Models
{
    public class BrandDTO
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public List<ProduktDTO> Produkts { get; set; }
    }
}
