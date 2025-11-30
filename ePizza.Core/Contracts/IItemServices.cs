using ePizza.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Core.Contracts
{
    public interface IItemServices 
    {
        IEnumerable<ItemResponseModel> GetItems();

        IEnumerable<ItemResponseModel> GetItemsUsingAdo();

        IEnumerable<ItemResponseModel> GetItemsUsingProcedure();
    }
}
