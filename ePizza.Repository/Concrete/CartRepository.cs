using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using epizza.Domain.Models;
using ePizza.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ePizza.Repository.Concrete
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(epizzaHubDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> DeleteItemAsync(Guid CartId, int ItemId)
        {
            var items=await _dbContext.CartItems.FirstOrDefaultAsync(x=>x.CartId==CartId && x.ItemId==ItemId);
            if (items != null)
            {
                _dbContext.CartItems.Remove(items);
                int recordsAffected=await _dbContext.SaveChangesAsync();
                return recordsAffected > 0;
            }
            return false;
        }

        public async Task<Cart> GetCartDetailsAsync(Guid cartId)
        {
            return await _dbContext
                       .Carts
                       .Include(x => x.CartItems)
                       .ThenInclude(x=> x.Item)
                       .Where(
                              x => x.Id == cartId && x.IsActive == true)
                      .FirstOrDefaultAsync();
        }
        public async Task<int> GetCartItemsQuantity(Guid cartId)
        {
            return await _dbContext.CartItems.Where(x => x.CartId == cartId).CountAsync();
        }

        public async Task<int> UpdateItemQuantity(Guid cartId,int itemId, int quantity)
        {
            var currentItems=await _dbContext.CartItems.Where(x => x.CartId==cartId && x.ItemId==itemId).FirstOrDefaultAsync();
            currentItems.Quantity= quantity;
            _dbContext.Entry(currentItems).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }

        public Task<bool> UpdateItemCountAsync(Guid CartId, int ItemId, int NewQty)
        {
            throw new NotImplementedException();
        }
    }
}
