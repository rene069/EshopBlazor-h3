using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalayer.Models
{
    public class Produkt
    {
        public int ProduktId { get; set; }
        public string ProduktName { get; set; }
        public string Description { get; set; } = "bla bla bla description";
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public int TypesId { get; set; }
        public string? ImageURL { get; set; }
        public bool IsSoftDeleted { get; set; } = false;

        public Brand? Brand { get; set; } 
        public Types? Type { get; set; }
        public UserProdukt? UserProdukt { get; set; }


    }
}
