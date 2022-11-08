﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalayer.Models
{
    public class UserProdukt
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProduktId { get; set; }
        public Produkt Produkt { get; set; }

        public int Quantity { get; set; }
    }
}
