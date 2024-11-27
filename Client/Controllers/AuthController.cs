using Client.ApiServices.Interfaces;
using Client.Models;
using Client.Models.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

            var jwtHandler = new JwtSecurityTokenHandler();
            if (response.Data.Token==null)
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });
            }
            var jwtToken = jwtHandler.ReadJwtToken(response.Data.Token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            if (roleClaim != null)
            {
                HttpContext.Session.SetString("UserRole", roleClaim.Value);
            }

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
