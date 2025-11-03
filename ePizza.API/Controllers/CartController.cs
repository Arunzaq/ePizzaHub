using ePizza.Core.Contracts;
using ePizza.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace ePizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartServices _cartServices;

        public CartController(ICartServices cartServices)
        {
            _cartServices = cartServices;
        }

        [HttpGet]
        [Route("get-cart-details")]
        public async Task<IActionResult> GetCartDetailsAsync(Guid CartId)
        {
            var Data = await _cartServices.GetCartDetailAsync(CartId);
            return Ok(Data);
        }

        [HttpPost]
        [Route("add-item-to-cart")]
        public async Task<IActionResult> AddItemToCart([FromBody]AddToCartRequest addToCartRequest)
        {
            var Data = await _cartServices.AddItemToCartAssync(addToCartRequest);
            return Ok(Data);
        }
    }
}
