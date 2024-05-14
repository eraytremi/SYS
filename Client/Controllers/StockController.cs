using Client.ApiServices.Interfaces;
using Client.Filters;
using Client.Models;
using Client.Models.Dtos;
using Client.Models.Dtos.Product;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Client.Controllers
{
    [SessionAspect]
    public class StockController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public StockController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Post(PostProduct dto)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ResponseBody<PostProduct>>("/Products", JsonSerializer.Serialize(dto), token.Token);
            if (response.StatusCode == 201)
            {
                return Json(new { IsSuccess = true, Message = "Başarıyla Kaydedildi", response.Data });

            }
            else
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });

            }
        }
    }
}
