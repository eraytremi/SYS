using Client.Models.Dtos.User;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Client.ApiServices.Interfaces;

namespace Client.Controllers
{
    public class GroupMemberController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public GroupMemberController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(long userId,long groupId)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.DeleteDataAsync<ResponseBody<NoContent>>($"/GroupMembers/{userId}?groupId={groupId}", token.Token);

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
