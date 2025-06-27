using epizza.Domain.Models;
using ePizza.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Repository.Concrete
{
    public class RoleRepository : GenericRepository<Role>, IRolesRepository
    {
        public RoleRepository(epizzaHubDBContext dbContext) : base(dbContext)
        {
        }
    }
}
