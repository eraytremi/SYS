using Client.ApiServices.Interfaces;
using Client.Models;
using Client.Models.Dtos;
using Client.Models.Dtos.StockMovement;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class StockMovementController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public StockMovementController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response =  await _httpApiService.GetDataAsync<ResponseBody<List<GetStockMovement>>>("/StockMovements", token.Token);
            return View(response.Data);
        }
    }
}
