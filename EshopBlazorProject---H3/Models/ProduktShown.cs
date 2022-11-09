namespace EshopBlazor.Models
{
    public class ProduktShown
    {
        public int ProduktId { get; set; }
        public string ProduktName { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string TypeName { get; set; }
        public bool IsSoftDeleted { get; set; }
    }
}
