using ePizza.Core.Contracts;
using ePizza.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

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

        [HttpGet]
        [Route("get-cart-Count")]
        public async Task<IActionResult> GetItemCount(Guid CartId)
        {
            var Data = await _cartServices.GetitemCount(CartId);
            return Ok(Data);
        }

        [HttpPost]
        [Route("add-item-to-cart")]
        public async Task<IActionResult> AddItemToCart([FromBody]AddToCartRequest addToCartRequest)
        {
            var Data = await _cartServices.AddItemToCartAssync(addToCartRequest);
            return Ok(Data);
        }

        [HttpPut]
        [Route("delete-item")]
        public async Task<IActionResult> DeleteItem(Guid CartId,int ItemId)
        {
            var Data = await _cartServices.DeleteItemFromCArtAsync(CartId,ItemId);
            return Ok(Data);
        }

        [HttpPut]
        [Route("update-item")]
        public async Task<IActionResult> UpdateItem(UpdateCartItemRequest updateCartItemRequest)
        {
            var Data = await _cartServices.UpdateItemInCartAsync(
                updateCartItemRequest.CartId,updateCartItemRequest.ItemId,updateCartItemRequest.Quantity);
            return Ok(Data);
        }

        [HttpPost]
        [Route("place-order")]
        public async Task<IActionResult> PlaceOrder(UpdateCartItemRequest updateCartItemRequest)
        {
            var Data = await _cartServices.UpdateItemInCartAsync(
                updateCartItemRequest.CartId, updateCartItemRequest.ItemId, updateCartItemRequest.Quantity);
            return Ok(Data);
        }

    }
}
