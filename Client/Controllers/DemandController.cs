using Client.ApiServices.Interfaces;
using Client.Models;
using Client.Models.Dtos.Demand;
using Client.Models.Dtos.Product;
using Client.Models.Dtos.User;
using Client.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace Client.Controllers
{
    public class DemandController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public DemandController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        } 

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var responseProduct = await _httpApiService.GetDataAsync<ResponseBody<List<GetProduct>>>("/Products", token.Token);

            return View(responseProduct.Data);
        }

        [HttpGet]
        public async Task<IActionResult> WaitingDemands()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.GetDataAsync<ResponseBody<List<GetDemand>>>("/Demands/waitingDemands", token.Token);

            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> ApprovedDemands()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.GetDataAsync<ResponseBody<List<GetDemand>>>("/Demands/approvedDemands", token.Token);
            return View(response.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Post(PostDemand demand)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ResponseBody<PostDemand>>("/Demands", JsonSerializer.Serialize(demand), token.Token);
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
        public async Task<IActionResult> Update(UpdateDemand demand)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PutDataAsync<ResponseBody<UpdateDemand>>("/Demands", JsonSerializer.Serialize(demand), token.Token);
            if (response.StatusCode == 200)
            {
                return Json(new { IsSuccess = true, Message = "Başarıyla Güncellendi", response.Data });
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
            var response = await _httpApiService.DeleteDataAsync<ResponseBody<NoContent>>($"/Demands/{id}", token.Token);

            if (response.StatusCode == 200)
            {
                return Json(new { IsSuccess = true, Message = "Başarıyla Silindi", response.Data });
            }
            else
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });

            }
        }

        [HttpGet]
        public async Task<IActionResult> Approve(int id)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.DeleteDataAsync<ResponseBody<NoContent>>($"/Demands/ApproveDemand/{id}", token.Token);
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
            var response = await _httpApiService.DeleteDataAsync<ResponseBody<NoContent>>($"/Demands/RejectDemand/{id}", token.Token);
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
