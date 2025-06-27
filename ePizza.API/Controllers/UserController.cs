using Microsoft.AspNetCore.Mvc;
using ePizza.Core.Contracts;
using ePizza.Models.Request;

namespace ePizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task< IActionResult >Get()
        {
            var UserResponse= _userService.GetAllUsers();

            return Ok(UserResponse);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateUserRequest createUserRequest)
        {
            if (ModelState.IsValid)
            {
                var CreateUser = _userService.AddUser(createUserRequest);
                return Ok();
            }
            return BadRequest(ModelState.Select(x=>x.Key));
            
        }
    }
}
