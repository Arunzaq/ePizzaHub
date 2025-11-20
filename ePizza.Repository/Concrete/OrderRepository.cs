using epizza.Domain.Models;
using ePizza.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Repository.Concrete
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(epizzaHubDBContext dbContext) : base(dbContext)
        {

        }
        public async Task<bool> AddNewOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            int rowsAffected = await _dbContext.SaveChangesAsync();

            return rowsAffected > 0;
        }
    }
}
