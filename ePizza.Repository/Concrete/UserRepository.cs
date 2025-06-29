
using epizza.Domain.Models;
using ePizza.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Repository.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        // add
        // delete 
        // get 
        // update
        public UserRepository(epizzaHubDBContext dbContext) : base(dbContext)
        {
        }

        public User findUser(string emailAdress)
        {
            return _dbContext.Users.Where(x=>x.Email==emailAdress).FirstOrDefault()!;
        }
    }
}
