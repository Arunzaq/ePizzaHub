using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using epizza.Domain.Models;
using ePizza.Core.Contracts;
using ePizza.Core.CustomException;
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

        public async Task<bool> DeleteItemFromCArtAsync(Guid CartId, int ItemId)
        {
            var isDeleted= await _cartRepository.DeleteItemAsync(CartId, ItemId);
            if (!isDeleted)
            {
                throw new Exception($"Item with ItemId{ItemId} doesnt exists in cart with id {CartId}");

            }
            return isDeleted;
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

        public async Task<int> GetitemCount(Guid cartId)
        {
            return await _cartRepository.GetCartItemsQuantity(cartId);
        }

        public Task<bool> UpdateItemCountAsync(Guid CartId, int ItemId, int NewQty)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateItemInCartAsync(Guid cartId, int itemId, int quantity)
        {
            var cartExists = await _cartRepository.GetAllAsync(x  => x.Id == cartId);
            if(!cartExists.Any())
            {
                throw new RecordNotFoundException($"Cart withId {cartId} doesnt exists");
            }
            int recordsUpdated = await _cartRepository.UpdateItemQuantity(cartId, itemId, quantity);
            return recordsUpdated > 0;
           
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
            else
            { 
            CartItem cartItem= cartDetails.CartItems.Where(x=> x.ItemId==request.ItemId).FirstOrDefault()!;
                if (cartItem == null)
                {
                    cartItem = new CartItem()
                    {
                       CartId=request.CartId,
                       ItemId=request.ItemId,
                       Quantity=request.Quantity,
                       UnitPrice=request.UnitPrice,
                    };
                    cartDetails.CartItems.Add(cartItem);
                }
                else
                {
                    cartItem.Quantity += request.Quantity;
                }
                _cartRepository.Update(cartDetails);
                int itemsAdded = _cartRepository.Commitchanges();
                return itemsAdded > 0;
            }
          
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
