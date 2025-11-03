using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using epizza.Domain.Models;
using ePizza.Models.Response;

namespace ePizza.Core.Mapper
{
    public static class CartMappingExtension
    {
        public static CartResponseModel ConvertToCartResponseModel(this Cart CartDetails)
        {
            CartResponseModel response = new CartResponseModel();

            if (CartDetails != null)
            {
                response.Id = CartDetails.Id;
                response.UserId = CartDetails.UserId;
                response.CreatedDate = CartDetails.CreatedDate;
                response.Items = CartDetails.CartItems.Select(
                    x => new CartItemresponse
                    {
                        Id = x.Id,
                        ItemId = x.ItemId,
                        Quantity = x.Quantity,
                        UnitPrice = x.UnitPrice,
                    }).ToList();
                response.Total = response.Items.Sum(x => x.Quantity * x.UnitPrice);
                response.Tax = Math.Round(response.Total * 0.05m, 2);
                response.GrandTotal = response.Total + response.Tax;

                return response;
            }
            return null;
        }
    }
}
