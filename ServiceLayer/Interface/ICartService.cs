using Datalayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface ICartService
    {
        
        List<UserProdukt> GetShoppingCartByUser(int userId);
        Task<List<UserProdukt>> GetShoppingCartByUserAsync(int userId);

        void UpdateUserProdukt(UserProdukt userProdukt);

        bool DoesProductExistInCart(int iD);

        UserProdukt GetUserProduktById(int userId, int produktId);
    }
}
