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
        private readonly ILogger<AuthController> _logger;   

        public AuthController(IAuthServices authServices,TokenGenerator tokenGenerator, ILogger<AuthController> logger ) 
        {
            _authServices = authServices;
            _tokenGenerator = tokenGenerator;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult ValidateUser(string UserName, string Password)
        {

            var userDetails=_authServices.validateUser(UserName, Password);
            if (userDetails !=null)
            { 
                _logger.LogInformation($"The Current Passed in Username is {UserName}");
            var securitytoken=_tokenGenerator.GenerateToken(userDetails);
                var response= new AuthApiResponseModel()
                { 
                AccessToken = securitytoken,
                };

                _logger.LogInformation($"Token generated successfully");
                return Ok(response);
            }
            return BadRequest("User respose is not valid");
        }
    }
}
