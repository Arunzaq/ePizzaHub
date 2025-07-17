using ePizza.Core.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ePizza.Core.Utils;

namespace ePizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        private readonly TokenGenerator _tokenGenerator;

        public AuthController(IAuthServices authServices,TokenGenerator tokenGenerator) 
        {
            _authServices = authServices;
            _tokenGenerator = tokenGenerator;
        }
        [HttpGet]
        public IActionResult ValidateUser(string UserName, string Password)
        {

            var userDetails=_authServices.validateUser(UserName, Password);
            if (userDetails)
            { 
            var Securitytoken=_tokenGenerator.GenerateToken();
                return Ok(Securitytoken);
            }
            return Ok(userDetails);
        }
    }
}
