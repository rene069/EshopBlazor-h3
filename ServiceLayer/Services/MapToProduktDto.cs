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
    public static class MapToProduktDto
    {
        public static IQueryable<ProduktDto> MapProduktsToDto(this IQueryable<Produkt> produkts)
        {
            return produkts.Select(x => new ProduktDto
            {
                ProduktName = x.ProduktName,
                Price = x.Price,
                ProduktId = x.ProduktId,
                BrandName = x.Brand.BrandName,
                TypeName = x.Type.TypeName,
                IsSoftDeleted = x.IsSoftDeleted,
                ImageUrl = x.ImageURL

            });
            
        }

        public static ProduktDto MapProduktToDto(this Produkt produkt)
        {

            return new ProduktDto
            {
                ProduktName = produkt.ProduktName,
                Price = produkt.Price,
                ProduktId = produkt.ProduktId,
                BrandName = produkt.Brand.BrandName,
                TypeName = produkt.Type.TypeName,
                IsSoftDeleted = produkt.IsSoftDeleted,
                ImageUrl = produkt.ImageURL,
                Description = produkt.Description
            };

        }
    }
}
