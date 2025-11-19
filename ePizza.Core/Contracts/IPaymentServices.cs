using ePizza.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Core.Contracts
{
    public interface IPaymentServices
    {
        Task<bool> MakePaymentAsync(MakePaymentRequest PaymentRequest);
    }
}
