using ePizza.UI.Helpers;
using ePizza.UI.Models;
using ePizza.UI.Models.ApiResponses;
using ePizza.UI.RazorPay;
using Microsoft.AspNetCore.Mvc;

namespace ePizza.UI.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IRazorPayService _razorPayService;

        public PaymentController(IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IRazorPayService razorPayService)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _razorPayService = razorPayService;
        }

        public IActionResult Index()
        { 
            PaymentModel payment = new PaymentModel();
            GetCartResponseModel cart = TempData.Peek<GetCartResponseModel>("CartId");
            if(cart!=null)
            {
                payment.RazorpayKey = _configuration["RazorPay:key"];
                payment.Cart = cart;
                payment.GrandTotal=Math.Round(cart.GrandTotal);
                payment.Currency = "INR";
                payment.Description = string.Join(",", cart.Items.Select(r => r.ItemName));
                payment.Receipt=Guid.NewGuid().ToString();

                payment.OrderId=_razorPayService.CraetwOrder(payment.GrandTotal*100,payment.Currency,payment.Receipt);
                return View(payment);
            }

            return RedirectToAction("Index", "Cart");
        }
    }
}
