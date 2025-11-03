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

        public async Task<Cart> GetCartDetailsAsync(Guid cartId)
        {
            return await _dbContext
                       .Carts
                       .Include(x => x.CartItems)
                       .Where(
                              x => x.Id == cartId && x.IsActive == true)
                      .FirstOrDefaultAsync();
        }
    }
}
