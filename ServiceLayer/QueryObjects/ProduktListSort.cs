using Datalayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.QueryObjects
{

    public enum OrderByOptions
    {
        [Display(Name = "Sort by ProduktName")]
        SimpleOrder = 0,
        [Display(Name = "Sort by ProduktName Desc")]
        ByNameDesc,
        [Display(Name = "Price")]
        ByPrice,
        [Display(Name = " Brand")]
        ByBrand,
        
            

    }
    public static class ProduktListSort
    {
        public static List<Produkt> OrderProduktsBy(this List<Produkt> produkts, OrderByOptions orderByOptions)
        {
            switch (orderByOptions)
            {
                case OrderByOptions.SimpleOrder:
                    return produkts.OrderBy(x => x.ProduktName).ToList();
                case OrderByOptions.ByNameDesc:
                    return produkts.OrderByDescending(x => x.ProduktName).ToList();
                case OrderByOptions.ByPrice:
                    return produkts.OrderBy(x => x.Price).ToList();                    
                case OrderByOptions.ByBrand:
                    return produkts.OrderBy(x => x.Brand.BrandName).ToList();
               default:
                    throw new ArgumentOutOfRangeException(nameof(orderByOptions), orderByOptions, null);
            }
        }
    }
}
