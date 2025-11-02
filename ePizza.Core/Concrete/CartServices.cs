using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePizza.Core.Contracts;
using ePizza.Repository.Concrete;
using ePizza.Repository.Contracts;

namespace ePizza.Core.Concrete
{
    public class CartServices :ICartServices
    {
        private readonly CartRepository _cartRepository;

        public CartServices(CartRepository cartRepository) 
        {
            _cartRepository = cartRepository;
        }
    }
}
