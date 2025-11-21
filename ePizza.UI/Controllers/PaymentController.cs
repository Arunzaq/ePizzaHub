using Microsoft.AspNetCore.Mvc;

namespace ePizza.UI.Controllers
{
    public class PaymentController : BaseController
    {
       public IActionResult Index()
        { 
        return View();
        }
    }
}
