using ePizza.UI.Helpers.TokenHelpers;
using ePizza.UI.Models.ApiResponses;
using ePizza.UI.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ePizza.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenServices _tokenServices;

        public LoginController(IHttpClientFactory httpClientFactory, ITokenServices tokenServices) 
        {
            _httpClientFactory = httpClientFactory;
            _tokenServices = tokenServices;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult >Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient("ePizzaApiClient");
                //var UserValid = await client.GetFromJsonAsync<bool>($"api/Auth?UserName={loginModel.UserName}&Password={loginModel.Password}");
                var userResponse = await client.GetFromJsonAsync<ApiResponseModel<ValidateUserResponseModel>>(
                    $"api/Auth?userName={loginModel.UserName}&password={loginModel.Password}");

                if (userResponse.Success)
                {
                    var accessToken=userResponse.Data.AccessToken;
                    _tokenServices.SetToken(accessToken);
                    var TokenHandler = new JwtSecurityTokenHandler();

                    var tokenDetails=TokenHandler.ReadToken(accessToken) as JwtSecurityToken;

                    List<Claim> claims = new List<Claim>();
                    foreach (var item in tokenDetails.Claims)
                    {
                        claims.Add(new Claim(item.Type, item.Value));
                    }
                   await GenerateTicket(claims);

                    bool isAdmin = Convert.ToBoolean( claims.Where(x => x.Type == "IsAdmin").FirstOrDefault().Value);
                    if (isAdmin)
                    {
                        return RedirectToAction("Index","Home",new {area=("Admin")});
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home", new { area = ("User") });
                    }
                }
            }
            return View();
        }

        private async Task GenerateTicket(List<Claim> claims)
        {
            var identity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var principal=new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties()
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
                });

        }

        [HttpGet]
        [Authorize]
        public IActionResult WelcomScreen()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // we have to do remove token from cookies
            return RedirectToAction("Login", "Login");
        }
    }
}
