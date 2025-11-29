using ePizza.UI.Helpers;
using ePizza.UI.Models;
using ePizza.UI.Models.ApiRequest;
using ePizza.UI.Models.ApiResponses;
using ePizza.UI.Models.ViewModel;
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

        public async Task<IActionResult>Status(IFormCollection form)
        {
            if(form.Keys.Count>0)
            {
                string paymentId = form["rzp_paymentid"];
                string orderId = form["rzp_orderid"];
                string signature = form["rzp_signature"];
                string transactionId = form["Receipt"];
                string currency = form["Currency"];

                bool isSignatureValid = _razorPayService.VerifySignature(signature, orderId, paymentId);

                if (isSignatureValid)
                {
                    var payment = _razorPayService.GetPayment(paymentId);
                    string status = payment["status"];

                    var paymentRequestModel = GetPaymentRequest(paymentId, orderId, transactionId, currency, status);
                    using var httpClient = _httpClientFactory.CreateClient("ePizaApiClient");
                    var paymentRequest = await httpClient.PostAsJsonAsync($"api/Payment", paymentRequestModel);
                    paymentRequest.EnsureSuccessStatusCode();
                    Response.Cookies.Delete("CartId");
                    TempData.Remove("CartId");
                    TempData.Remove("Address");

                    return RedirectToAction("Receipt");

                }
            }

            ViewBag.Message = "Payment Failed";
            return View();
        }

        public IActionResult Receipt()
        {
            return View();
        }

        private MakePaymentRequestModel GetPaymentRequest(
            string paymentId,
            string orderId,
            string transactionId,
            string currency,
            string status)
        {

            GetCartResponseModel cart = TempData.Peek<GetCartResponseModel>("CartId");
            AddressViewModel addressViewModel = TempData.Peek<AddressViewModel>("Address");

            return new MakePaymentRequestModel()
            {
                CartId = cart.Id,
                Total = cart.Total,
                Tax = cart.Tax,
                GrandTotal = cart.GrandTotal,
                Currency = currency,
                CreatedDate = DateTime.UtcNow,
                Status = status,
                Email = CurrentUser.Email,
                UserId = CurrentUser.UserId,
                Id = paymentId,
                TransactionId = transactionId,
                OrderRequest = new OrderRequest()
                {
                    Id = orderId,
                    Street = addressViewModel.Street,
                    City = addressViewModel.City,
                    Locality = addressViewModel.Locality,
                    ZipCode = addressViewModel.ZipCode,
                    UserId = CurrentUser.UserId,
                    PhoneNumber = addressViewModel.PhoneNumber,
                    OrderItems = GetOrderItems(cart.Items)
                }
            };
        }

        private List<OrderItems> GetOrderItems(List<CartItemresponse> cartItems)
        {
            List<OrderItems> orderItems = new();
            foreach(var item in cartItems)
            {
                OrderItems items = new()
                { 
                ItemId = item.Id,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Total=item.ItemTotal
                };
                orderItems.Add(items);
            }
            return orderItems;
        }
    }
}
