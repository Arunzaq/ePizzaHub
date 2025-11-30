using ePizza.Core.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ePizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemServices _itemServices;

        public ItemController(IItemServices itemServices)
        {
            _itemServices = itemServices;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var items = _itemServices.GetItems();
            return Ok(items);
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var items = _itemServices.GetItemsUsingAdo();
        //    return Ok(items);
        //}
    }
}
