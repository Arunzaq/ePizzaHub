using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Core.Contracts
{
    public interface IAuthServices
    {
        bool validateUser(string username, string password);
    }
}
