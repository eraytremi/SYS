using Client.Models;
using Client.Models.Dtos;
using Client.Models.Dtos.Supplier;
using Microsoft.AspNetCore.Mvc;
using PurchasingSystem.Web.ApiServices.Interfaces;
using System.Text.Json;

namespace Client.Controllers
{
    public class SupplierController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public SupplierController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }
                        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.GetDataAsync<ResponseBody<List<GetSupplier>>>("/Suppliers", token.Token);
            return View(response.Data);
        }

        [HttpPost]
        public IActionResult PostSupplier ()
        {
            
            return View();
        }
    }
}
