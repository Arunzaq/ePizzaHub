using ePizza.Models.Response;
using ePizza.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Core.Contracts
{
    public interface IUserService
    {
        IEnumerable<UserResponseModel> GetAllUsers();

        bool AddUser(CreateUserRequest userRequest);

    }
}
