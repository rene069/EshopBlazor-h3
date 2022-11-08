using Datalayer;
using Datalayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository
{
    public class CartService : ICartService
    {
        private readonly EShopContext _eShopContext;
        public CartService(EShopContext eShopContext)
        {
            _eShopContext = eShopContext;
        }
       

        public void UpdateUserProdukt(UserProdukt userProdukt)
        {
            _eShopContext.Update(userProdukt);
            _eShopContext.SaveChanges();
                
        }

        public bool DoesProductExistInCart(int iD)
        {
            return _eShopContext.UserProdukts.Where(x => x.ProduktId == iD).Any();
        }

        public UserProdukt GetUserProduktById(int userId,int produktId)
        {
            return _eShopContext.UserProdukts.Where(x => x.UserId == userId && x.ProduktId == produktId).FirstOrDefault();
        }
        public async Task<List<UserProdukt>> GetShoppingCartByUserAsync(int userId) => await _eShopContext.UserProdukts.Where(x => x.UserId == userId).Include(x => x.Produkt).ToListAsync();
        public List<UserProdukt> GetShoppingCartByUser(int userId) => _eShopContext.UserProdukts.Where(x => x.UserId == userId).Include(x => x.Produkt).ToList();

       
    }
}
