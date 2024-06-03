using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class ExitController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
    }
}
