using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Domain.StoredProcedures
{
    public class GetOrderDetailsDto
    {
        public string PaymentId { get; set; } = default!;
        public string ZipCode { get; set; } = default!;

        public string City { get; set; } = default!;

        public string Street { get; set; } = default!;

        public int ItemId { get; set; }

        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
