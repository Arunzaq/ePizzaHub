using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePizza.Core.Concrete;
using ePizza.Models.Request;
using ePizza.Models.Response;

namespace ePizza.Core.Contracts
{
    public interface ICartServices 
    {
        Task<CartResponseModel> GetCartDetailAsync(Guid cartId);
        Task<bool> AddItemToCartAssync(AddToCartRequest request);
        Task<bool> DeleteItemFromCArtAsync(Guid CartId,int ItemId);
        Task<bool> UpdateItemCountAsync(Guid CartId, int ItemId,int NewQty);
        Task<bool> UpdateItemInCartAsync(Guid cartId,int itemId,int quantity);
        Task<int> GetitemCount(Guid cartId);
        Task<int> UpdateCartUser(Guid cartId, int userId);
    }
}
