using ePizza.Core.Contracts;
using ePizza.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ePizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentServices _paymentServices;

        public PaymentController(IPaymentServices paymentServices) 
        {
            _paymentServices = paymentServices;
        }

        [HttpPost]
        public async Task <IActionResult> MakePayment([FromBody] MakePaymentRequest makePaymentRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _paymentServices.MakePaymentAsync(makePaymentRequest);
                return Ok();
            }
            return BadRequest("Please check Values");
            
        }


    }
}
