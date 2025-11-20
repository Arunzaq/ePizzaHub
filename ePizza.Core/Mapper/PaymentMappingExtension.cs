using AutoMapper;
using epizza.Domain.Models;
using ePizza.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Core.Mapper
{
    public class PaymentMappingExtension : Profile
    {
        public PaymentMappingExtension() 
        {
            CreateMap<MakePaymentRequest, PaymentDetail>();
            CreateMap<OrderRequest, Order>();
            CreateMap<OrderItems, OrderItem>();
        }
        
    }
}
