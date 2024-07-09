using Client.ApiServices.Interfaces;
using Client.Models;
using Client.Models.Dtos.Product;
using Client.Models.Dtos.SalesDetails;
using Client.Models.Dtos.StockStatus;
using Client.Models.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Client.Controllers
{
    public class SaleDetailsController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public SaleDetailsController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var product = await _httpApiService.GetDataAsync<ResponseBody<List<GetProduct>>>("/Products", token.Token);
            return View(product.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(List<PostSalesDetailsModel> dto)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ResponseBody<List<PostSalesDetailsModel>>>("/SaleDetails", JsonSerializer.Serialize(dto), token.Token);

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
