using epizza.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Repository.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User findUser(string emailAdress);
    }
}
