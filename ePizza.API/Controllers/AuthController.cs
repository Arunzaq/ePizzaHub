using ePizza.Core.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ePizza.Core.Utils;
using ePizza.Models.Response;
using ePizza.Core.Concrete;
using System.Security.Claims;
using System.Text.Json;

namespace ePizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        private readonly TokenGenerator _tokenGenerator;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthServices authServices,TokenGenerator tokenGenerator, ILogger<AuthController> logger,IConfiguration configuration ) 
        {
            _authServices = authServices;
            _tokenGenerator = tokenGenerator;
            _logger = logger;
            _configuration = configuration;
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
                    TokenExpiryInSeconds = Convert.ToInt32(_configuration["Jwt:TokenExpiryInMinutes"]),
                    RefreshToken = _tokenGenerator.GenerateRefreshToken()
                };

                PersistUserToken(userDetails, response);

                _logger.LogInformation($"Token generated successfully");
                return Ok(response);
            }
            return BadRequest("User respose is not valid");
        }

        private void PersistUserToken(
         ValidateUserResponse userDetails,
         AuthApiResponseModel authApiResponse)
        {
            _authServices.PersistUserToken(new UserTokenModel()
            {
                AccessToken = authApiResponse.AccessToken,
                RefreshToken = authApiResponse.RefreshToken,
                UserId = userDetails.UserId,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(7)
            });
        }

        [HttpPost("token-refresh")]
        public IActionResult Refresh([FromBody] RefreshTokenRequest tokenModel)
        {
            var principal = _tokenGenerator.GetTokenPrincipal(tokenModel.AccessToken);

            if (principal == null) return Unauthorized("Invalid access token");

            var username = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var rolesClaim = principal.Claims.FirstOrDefault(x => x.Type == "Roles").Value;

            List<string> roles = rolesClaim != null
                ? JsonSerializer.Deserialize<List<string>>(rolesClaim)
                : new List<string>();

            var previousTokenDetails = _authServices.GetSavedTokenDetail(username);

            if (previousTokenDetails == null
                || previousTokenDetails.RefreshToken != tokenModel.RefreshToken
                || previousTokenDetails.RefreshTokenExpiryTime < DateTime.UtcNow)
                return Unauthorized("Invalid refresh token");

            var userDetails = _authServices.GetUserDetails(username);
            userDetails.Roles = roles;

            var newAccessToken = _tokenGenerator.GenerateToken(GetUserResponseObject(userDetails));
            var newRefreshToken = _tokenGenerator.GenerateRefreshToken();

            previousTokenDetails.RefreshToken = newRefreshToken;
            previousTokenDetails.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            _authServices.PersistUserToken(previousTokenDetails);
            // udpate token in my database

            return Ok(new AuthApiResponseModel
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                TokenExpiryInSeconds = Convert.ToInt32(_configuration["Jwt:TokenExpiryInMinutes"]),
            });
        }

        private ValidateUserResponse GetUserResponseObject(UserResponseModel userResponse)
        {
            return new ValidateUserResponse
            {
                Email = userResponse.Email,
                Name = userResponse.Name,
                Roles = userResponse.Roles,
                UserId = userResponse.Id
            };
        }
    }
}
