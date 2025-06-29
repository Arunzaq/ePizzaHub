using ePizza.UI.Models;
using Microsoft.AspNetCore.Mvc;

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
                var UserValid = await client.GetFromJsonAsync<bool>($"api/Auth?UserName={loginModel.UserName}&Password={loginModel.Password}");
                if (UserValid)
                {
                   return RedirectToAction("WelcomeScreen");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult WelcomeScreen()
        {
            return View();
        }
    }
}
