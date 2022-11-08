namespace ServiceLayer.DTO
{
    public class ProduktDto
    {
        public int ProduktId { get; set; }
        public string ProduktName { get; set; }
        public string BrandName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string TypeName { get; set; }
        public int BrandId { get; set; }
        public int TypesId { get; set; }
        public bool IsSoftDeleted { get; set; }

    }
}