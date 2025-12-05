using ePizza.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Core.Contracts
{
    public interface IAuthServices
    {
        ValidateUserResponse validateUser(string username, string password);

        bool PersistUserToken(UserTokenModel userTokenModel);
        UserTokenModel GetSavedTokenDetail(string userName);
        UserResponseModel GetUserDetails(string userName);
    }
}
