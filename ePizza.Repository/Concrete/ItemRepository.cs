using epizza.Domain.Models;
using ePizza.Domain.StoredProcedures;
using ePizza.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Repository.Concrete
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(epizzaHubDBContext dbContext) : base(dbContext)
        {
        }


        public List<GetOrderDetailsDTO> CallProcedure()
        {
            var response = _dbContext.Database.SqlQueryRaw<GetOrderDetailsDTO>("exec sp_GetOrderDetails 'order_Q74yfsC1ABl1xc'").ToList();
            return response;
        }

    }
}
