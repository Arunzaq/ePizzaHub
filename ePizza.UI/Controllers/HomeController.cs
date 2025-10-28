using ePizza.UI.Models;
using ePizza.UI.Models.ApiResponses;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ePizza.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger , IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
             _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("ePizzaApiClient");
            var items = await client.GetFromJsonAsync<ApiResponseModel<IEnumerable<ItemResponseModel>>>("api/item");
            return View(items.Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
