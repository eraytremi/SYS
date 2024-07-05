using Client.ApiServices.Interfaces;
using Client.Models;
using Client.Models.Dtos.Product;
using Client.Models.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class SaleDetailsController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public SaleDetailsController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var product = await _httpApiService.GetDataAsync<ResponseBody<List<GetProduct>>>("/Products", token.Token);
            return View(product.Data);
        }
    }
}
