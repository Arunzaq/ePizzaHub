using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using epizza.Domain.Models;

namespace ePizza.Repository.Contracts
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        // i need to do some implementation
        //   get Cart(CartId)
        //deleteItem(CartId,Itemid)
        //updatequantity(CartId,ItemId,Qty)
        //UpdateCart

        Task<Cart> GetCartDetailsAsync(Guid cartId);
        Task<bool> DeleteItemAsync(Guid CartId, int ItemId);
      
    }
}
