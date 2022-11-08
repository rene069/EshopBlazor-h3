using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalayer.Models
{
    public class Types
    {
        public int TypesId { get; set; }
        public string TypeName { get; set; }

        public List<Produkt> Produkts { get; set; }
    }
}
