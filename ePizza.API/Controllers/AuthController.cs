using ePizza.Core.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ePizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices) 
        {
            _authServices = authServices;
        }
        [HttpGet]
        public IActionResult ValidateUser(string UserName, string Password)
        {

            var userDetails=_authServices.validateUser(UserName, Password);
            return Ok(userDetails);
        }
    }
}
