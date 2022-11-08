using Datalayer.Models;
using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public static class MapToProduktListDto
    {
        public static IQueryable<ProduktDto> MapProduktToDto(this IQueryable<Produkt> produkts)
        {
            return produkts.Select(x => new ProduktDto
            {
                ProduktName = x.ProduktName,
                Price = x.Price,
                ProduktId = x.ProduktId,
                BrandName = x.Brand.BrandName,
                TypeName = x.Type.TypeName,
                IsSoftDeleted = x.IsSoftDeleted,
                BrandId = x.BrandId,
                TypesId = x.TypesId,
                ImageUrl = x.ImageURL

            });
            
        }
    }
}
