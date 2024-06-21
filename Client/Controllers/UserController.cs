using Client.Models.Dtos.Supplier;
using Client.Models.Dtos.User;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Client.ApiServices.Interfaces;

namespace Client.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public UserController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.GetDataAsync<ResponseBody<List<UserGetDto>>>("/Users", token.Token);
            
            return View(response.Data);
        }


        [HttpPost]
        public async Task<IActionResult> RegisterUser(PostUserModel model)
        {

            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ResponseBody<PostUserModel>>("/Users", JsonSerializer.Serialize(model), token.Token);
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
        public async Task<IActionResult> Update(UpdateUser dto)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PutDataAsync<ResponseBody<UpdateUser>>("/Users", JsonSerializer.Serialize(dto), token.Token);
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
            var response = await _httpApiService.DeleteDataAsync<ResponseBody<NoContent>>($"/Users/{id}", token.Token);

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
