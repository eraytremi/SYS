using Microsoft.AspNetCore.Mvc;

namespace ClientDx.Controllers
{
	public class LogoutController : Controller
	{
		public IActionResult Index()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index","Auth");
		}
	}
}
