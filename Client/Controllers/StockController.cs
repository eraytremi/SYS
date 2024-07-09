using Client.ApiServices.Interfaces;
using Client.Filters;
using Client.Models;
using Client.Models.Dtos.Product;
using Client.Models.Dtos.StockStatus;
using Client.Models.Dtos.User;
using Client.Models.ViewModels;
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
            var stockStatus = await _httpApiService.GetDataAsync<ResponseBody<List<GetStockStatus>>>("/Stocks", token.Token);
            var product = await _httpApiService.GetDataAsync<ResponseBody<List<GetProduct>>>("/Products", token.Token);

            var response = new ProductStockVm
            {
                GetProducts = product.Data,
                GetStockStatuses = stockStatus.Data
            };

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostStockStatus dto)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ResponseBody<PostStockStatus>>("/Stocks", JsonSerializer.Serialize(dto), token.Token);

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
            var response = await _httpApiService.PutDataAsync<ResponseBody<UpdateStockStatus>>("/Stocks", JsonSerializer.Serialize(dto), token.Token);
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
            var response = await _httpApiService.DeleteDataAsync<ResponseBody<NoContent>>($"/Stocks/{id}", token.Token);

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
