using Client.ApiServices.Interfaces;
using Client.Filters;
using Client.Models;
using Client.Models.Dtos.Role;
using Client.Models.Dtos.User;
using Client.Models.Dtos.UserRole;
using Client.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Client.Controllers
{
    [SessionAspect]
    public class UserRoleController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public UserRoleController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.GetDataAsync<ResponseBody<List<GetUserRole>>>("/UserRoles", token.Token);
            var responseUser = await _httpApiService.GetDataAsync<ResponseBody<List<UserGetDto>>>("/Users", token.Token);
            var responseRole = await _httpApiService.GetDataAsync<ResponseBody<List<GetRole>>>("/Roles", token.Token);

            var vm = new UserRoleVM
            {
                Role = responseRole.Data,
                User = responseUser.Data,
                UserRole = response.Data

            };
            return View(vm);
        }



        [HttpPost]
        public async Task<IActionResult> Post(PostUserRole dto)
        {

            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ResponseBody<PostUserRole>>("/UserRoles", JsonSerializer.Serialize(dto), token.Token);
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
        public async Task<IActionResult> Update(UpdateUserRole dto)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PutDataAsync<ResponseBody<UpdateUserRole>>("/UserRoles", JsonSerializer.Serialize(dto), token.Token);
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
            var response = await _httpApiService.DeleteDataAsync<ResponseBody<NoContent>>($"/UserRoles/{id}", token.Token);

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
