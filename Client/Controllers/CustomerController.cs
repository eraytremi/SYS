using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
