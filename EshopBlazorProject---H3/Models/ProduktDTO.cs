using System;

namespace EshopBlazor.Models
{
    public class ProduktDTO
    {
        public int ProduktId { get; set; }
        public string ProduktName { get; set; }
        public string Description { get; set; } = "bla bla bla description";
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public int TypesId { get; set; }
        public string? ImageURL { get; set; }
        public bool IsSoftDeleted { get; set; } = false;

        public BrandDTO? Brand { get; set; }
        public TypesDTO? Type { get; set; }

    }
}
