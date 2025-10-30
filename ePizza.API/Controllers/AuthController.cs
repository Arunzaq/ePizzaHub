using ePizza.Core.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ePizza.Core.Utils;
using ePizza.Models.Response;

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
        public IActionResult ValidateUserResponse(string UserName, string Password)
        {

            var userDetails=_authServices.validateUser(UserName, Password);
            if (userDetails !=null)
            { 
            var Securitytoken=_tokenGenerator.GenerateToken(userDetails);
                var response= new AuthApiResponseModel()
                { 
                AccessToken = Securitytoken,
                };
                return Ok(response);
            }
            return BadRequest("User respose is not valid");
        }
    }
}
