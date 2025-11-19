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

        public PaymentServics(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<bool> MakePaymentAsync(MakePaymentRequest paymentRequest)
        {
            var paymentModel = _mapper.Map<PaymentDetail>(paymentRequest);
            paymentModel.Id=Guid.NewGuid().ToString();
            _paymentRepository.Add(paymentModel);
            int rowsAffected = _paymentRepository.Commitchanges();
            return await Task.FromResult(rowsAffected > 0);
        }
    }
}
