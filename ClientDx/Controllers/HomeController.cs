using ClientDx.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ClientDx.Controllers
{
	[SessionAspect]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
