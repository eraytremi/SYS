using Client.Models;
using Client.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using PurchasingSystem.Web.ApiServices.Interfaces;
using System.Text.Json;

namespace Client.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public AuthController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel loginModel)
        {
            var response = await _httpApiService.PostDataAsync<ResponseBody<UserGetDto>>("/Users/Login", JsonSerializer.Serialize(loginModel));

            if (response.StatusCode==200)
            {
                HttpContext.Session.SetObject("ActivePerson", response.Data);               
                return Json(new { IsSuccess = true, Messages = "Giriş başarılı" });
            }
            else
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });
            }
        }
    }
}
