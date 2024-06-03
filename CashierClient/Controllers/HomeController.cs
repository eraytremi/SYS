using CashierClient.Models;
using Client;
using Client.ApiServices.Interfaces;
using Client.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CashierClient.Controllers
{
    public class HomeController : Controller
    {

       private readonly IHttpApiService _httpApiService;

        public HomeController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        [HttpPost]
        public IActionResult Index()
        {
            var token =  HttpContext.Session.GetObject<UserGetDto>("token");
            var response
            return View();
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
