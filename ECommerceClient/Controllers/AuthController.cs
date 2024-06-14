using Microsoft.AspNetCore.Mvc;

namespace ECommerceClient.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
