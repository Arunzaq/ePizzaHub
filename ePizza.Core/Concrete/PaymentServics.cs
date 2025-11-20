using AutoMapper;
using epizza.Domain.Models;
using ePizza.Core.Contracts;
using ePizza.Models.Request;
using ePizza.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Core.Concrete
{
    public class PaymentServics :IPaymentServices
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public PaymentServics(IPaymentRepository paymentRepository, IMapper mapper, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public string MakePaymentAsync(MakePaymentRequest paymentRequest)
        {
            var paymentModel = _mapper.Map<PaymentDetail>(paymentRequest);
            if (paymentRequest.OrderRequest is not null
                && paymentRequest.OrderRequest.OrderItems.Count > 0)
            { 
            var orderDetails=MapOrderDetails(paymentRequest,paymentModel);
                _paymentRepository.Add(paymentModel);
                _orderRepository.Add(orderDetails);
                int rowsAffected = _paymentRepository.Commitchanges();
            }
            return string.Empty;
        }

        private Order MapOrderDetails(MakePaymentRequest paymentRequest, PaymentDetail paymentmodel)
        {
            var orderDetails = _mapper.Map<Order>(paymentRequest.OrderRequest);
            orderDetails.PaymentId=paymentmodel.Id;
            orderDetails.UserId=paymentmodel.UserId;
            orderDetails.OrderItems.ToList().ForEach(x=>x.OrderId=orderDetails.Id);
            return orderDetails;
        }
    }
}
