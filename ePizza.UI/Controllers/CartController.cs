using ePizza.UI.Models.ApiRequest;
using ePizza.UI.Models.ApiResponses;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace ePizza.UI.Controllers
{
    [Route("Cart")]
    public class CartController : Controller
    {
        

        private readonly ILogger<CartController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public CartController(ILogger<CartController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            using var httpClient = _httpClientFactory.CreateClient("ePizzaApiClient");
            var cartItems = await httpClient.GetFromJsonAsync<ApiResponseModel<GetCartResponseModel>>(
                $"api/Cart/get-cart-details?CartId={CartId}");
            return View(cartItems);
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
    }
}
