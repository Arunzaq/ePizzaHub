using ePizza.UI.Helpers;
using ePizza.UI.Models.ApiRequest;
using ePizza.UI.Models.ApiResponses;
using ePizza.UI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ePizza.UI.Controllers
{
    [Route("Cart")]
    public class CartController : BaseController
    {
        

        private readonly ILogger<CartController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public CartController(ILogger<CartController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            using var httpClient = _httpClientFactory.CreateClient("ePizzaApiClient");
            var cartItems = await httpClient.GetFromJsonAsync<ApiResponseModel<GetCartResponseModel>>(
                $"api/Cart/get-cart-details?CartId={CartId}");
            return View(cartItems.Data);
        }
        Guid CartId
        {
            get 
            {
                Guid id;
                string CartId = Request.Cookies["CartId"];
                if(CartId==null)
                {
                    id= Guid.NewGuid();
                    Response.Cookies.Append("CartId",id.ToString(),new CookieOptions { Expires = DateTime.Now.AddDays(1)});
                }
                else
                {
                    id = Guid.Parse(CartId);
                }
                return id;
            }
        }
        [HttpGet("AddToCart/{itemId:int}/{unitPrice:decimal}/{quantity:int}")]
        public async Task<IActionResult> AddToCart(int itemId, decimal unitPrice, int quantity)
        {
            using var httpClient = _httpClientFactory.CreateClient("ePizzaApiClient");
            AddToCartRequest addCartRequest = new AddToCartRequest()
            {

                ItemId = itemId,
                UnitPrice = unitPrice,
                Quantity = quantity,
                CartId = CartId
            };
            var itemAdded = await httpClient.PostAsJsonAsync<AddToCartRequest>("api/Cart/add-item-to-cart", addCartRequest);
            return Json(new { Count = 1});

        }
        [HttpDelete("DeleteItem/{itemId:int}")]
        public async Task<IActionResult> DeleteItem(int itemId, int quantity)
        {
            using var httpClient = _httpClientFactory.CreateClient("ePizaaApiClient");


            AddToCartRequest addCartRequest
                 = new AddToCartRequest()
                 {
                     ItemId = itemId,
                     Quantity = quantity,
                     CartId = CartId
                 };


            var itemAdded = await httpClient.PostAsJsonAsync<AddToCartRequest>("api/Cart/add-item-to-cart", addCartRequest);

            // TODO : get cart quanity API  to get no of item
            return Json(new { Count = 1 });

        }

        [HttpPut("UpdateQuantity/{itemId:int}/{quantity:int}")]
        public async Task<IActionResult> UpdateQuantity(int itemId, int quantity)
        {
            using var httpclient = _httpClientFactory.CreateClient("ePizzaApiClient");
            var updateCartitems =
                new
                {
                    CartId = CartId,
                    ItemId = itemId,
                    Quantity = quantity
                };
            var itemAdded = await httpclient.PutAsJsonAsync($"api/Cart/update-item", updateCartitems);
            return Json(new { Count = 1 });
        }

        [HttpGet("Checkout")]
        public IActionResult Checkout()
        {
            
            return View();
        }

        [HttpPost("Checkout")]
        public async Task <IActionResult> Checkout(AddressViewModel addressViewModel)
        {
            if (ModelState.IsValid && CurrentUser is not null)
            { 
            using var httpclient = _httpClientFactory.CreateClient("ePizzaApiClient");
                var cart = await httpclient.GetFromJsonAsync<ApiResponseModel<GetCartResponseModel>>(
                    $"api/Cart/get-cart-details?cartId={CartId}");
                if (cart.Success)
                {
                    var updateUserRequest
                            = new
                            {
                                 CartId,
                                 CurrentUser.UserId,
                            };
                    var response= await httpclient.PutAsJsonAsync($"api/Cart/update-cart_user",updateUserRequest);
                    response.EnsureSuccessStatusCode();

                    TempData.Set("Address", addressViewModel);
                    TempData.Set("CartId",cart.Data);
                }
                return RedirectToAction("Index" ,"Payment");
            }

            return View();
        }
    }
}
