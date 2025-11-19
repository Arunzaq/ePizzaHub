using epizza.Domain.Models;
using ePizza.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Repository.Concrete
{
    public class PaymentRepository : GenericRepository<PaymentDetail>, IPaymentRepository
    {
        public PaymentRepository(epizzaHubDBContext dBContext) : base(dBContext)
        {
            
        }
    }
}
