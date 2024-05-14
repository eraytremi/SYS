using ClientDvx.Models;
using ClientDvx.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ClientDvx.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpApiService _httpApiService;

		public AuthController(IHttpApiService httpApiService)
		{
			_httpApiService = httpApiService;
		}

		public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel loginModel)
        {
            var response = await _httpApiService.PostDataAsync<ResponseBody<UserGetDto>>("/Users/Login", JsonSerializer.Serialize(loginModel));

            if (response.StatusCode == 200)
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
