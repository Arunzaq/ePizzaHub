using ePizza.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ePizza.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory) 
        {
            _httpClientFactory = httpClientFactory;
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

                bool validUser = true;
                if (validUser)
                {
                    List<Claim> claims = new List<Claim>();
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal= new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
                    });
                   return RedirectToAction("WelcomScreen");
                }
            }
            return View();
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

            return RedirectToAction("Login", "Login");
        }
    }
}
