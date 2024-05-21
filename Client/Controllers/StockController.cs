using Client.ApiServices.Interfaces;
using Client.Filters;
using Client.Models;
using Client.Models.Dtos;
using Client.Models.Dtos.Product;
using Client.Models.Dtos.StockStatus;
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


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.GetDataAsync<ResponseBody<List<GetStockStatus>>>("/StockStatuses", token.Token);
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostStockStatus dto)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ResponseBody<PostStockStatus>>("/StockStatuses", JsonSerializer.Serialize(dto), token.Token);
            if (response.StatusCode == 201)
            {
                return Json(new { IsSuccess = true, Message = "Başarıyla Kaydedildi", response.Data });
            }
            else
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateStockStatus dto)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PutDataAsync<ResponseBody<UpdateStockStatus>>("/StockStatuses", JsonSerializer.Serialize(dto), token.Token);
            if (response.StatusCode == 200)
            {
                return Json(new { IsSuccess = true, Message = "Başarıyla Kaydedildi", response.Data });
            }
            else
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });
            }
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.DeleteDataAsync<ResponseBody<NoContent>>($"/StockStatuses/{id}", token.Token);

            if (response.StatusCode == 200)
            {
                return Json(new { IsSuccess = true, Message = "Başarıyla Silindi", response.Data });
            }
            else
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });

            }
        }
    }
}
