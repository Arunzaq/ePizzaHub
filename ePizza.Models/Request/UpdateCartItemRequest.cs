using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Models.Request
{
    public class UpdateCartItemRequest
    {
        public Guid CartId { get; set; }

        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
