using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using epizza.Domain.Models;
using ePizza.Core.Contracts;
using ePizza.Core.Mapper;
using ePizza.Models.Request;
using ePizza.Models.Response;
using ePizza.Repository.Concrete;
using ePizza.Repository.Contracts;

namespace ePizza.Core.Concrete
{
    public class CartServices :ICartServices
    {
        private readonly ICartRepository _cartRepository;

        public CartServices(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartResponseModel> GetCartDetailAsync(Guid cartId)
        {
            var cartDetails= await _cartRepository.GetCartDetailsAsync(cartId);

            if (cartDetails != null)
            { 
            return cartDetails.ConvertToCartResponseModel();
            }
            return null;
        }

        async Task<bool> ICartServices.AddItemToCartAssync(AddToCartRequest request)
        {
            var cartDetails = await _cartRepository.GetCartDetailsAsync(request.CartId);
            if (cartDetails == null)
            {
                // Create new cart
                int itemsAdded = AddNewCart(request);
                return itemsAdded > 0;
            }
            return false;
            //else
            //{ 
            //   //Update existing Cart
            //}
        }

        private int AddNewCart(AddToCartRequest request)
        {
            Cart ? cartDetails = new Cart
            {
                Id = request.CartId,
                UserId = request.UserId,
                CreatedDate = DateTime.UtcNow,
                IsActive = true,
            };
            CartItem Items = new CartItem
            {
                CartId = request.CartId,
                ItemId = request.ItemId,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
            };
            cartDetails.CartItems.Add(Items);
            _cartRepository.Add(cartDetails);
            return _cartRepository.Commitchanges();
        }
    }
}
