using Client.ApiServices.Interfaces;
using Client.Models;
using Client.Models.Dtos.StockMovement;
using Client.Models.Dtos.StockStatus;
using Client.Models.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Client.Controllers
{
    public class StockMovementController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public StockMovementController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response =  await _httpApiService.GetDataAsync<ResponseBody<List<GetStockMovement>>>("/StockMovements/approvedStatuses", token.Token);
            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> WaitingStatuses()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.GetDataAsync<ResponseBody<List<GetStockMovement>>>("/StockMovements/waitingStatuses", token.Token);
            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Approve(int id)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.DeleteDataAsync<ResponseBody<NoContent>>($"/StockMovements/ApproveStatus/{id}", token.Token);
            if (response.StatusCode == 200)
            {
                return Json(new { IsSuccess = true, Message = "Başarıyla Onaylandı", response.Data });
            }
            else
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });

            }
        }

        [HttpGet]
        public async Task<IActionResult> Reject(int id)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.DeleteDataAsync<ResponseBody<NoContent>>($"/StockMovements/RejectStatus/{id}", token.Token);
            if (response.StatusCode == 200)
            {
                return Json(new { IsSuccess = true, Message = "Başarıyla Reddedildi", response.Data });
            }
            else
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });

            }
        }
    }
}
