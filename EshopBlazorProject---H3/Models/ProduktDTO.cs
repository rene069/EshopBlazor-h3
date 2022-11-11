using System;
using System.ComponentModel.DataAnnotations;

namespace EshopBlazor.Models
{
    public class ProduktDTO
    {
        public int ProduktId { get; set; }
        [Required,MaxLength(25)]
        public string ProduktName { get; set; }
        [Required, MaxLength(100)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required]
        public int TypesId { get; set; }
        public string? ImageURL { get; set; }
        public bool IsSoftDeleted { get; set; } = false;


    }
}
