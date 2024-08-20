using Client.ApiServices.Interfaces;
using Client.Models;
using Client.Models.Dtos.GroupChat;
using Client.Models.Dtos.GroupMember;
using Client.Models.Dtos.User;
using Client.Models.Dtos.UserRole;
using Client.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Client.Controllers
{
    public class GroupChatController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public GroupChatController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");

            var groupChat = await _httpApiService.GetDataAsync<ResponseBody<List<GetGroupChat>>>("/GroupChats/getGroupChats", token.Token);
            var user = await _httpApiService.GetDataAsync<ResponseBody<List<UserGetDto>>>("/users", token.Token);

            var vm = new GroupChatUserVm
            {
                GetGroupChat = groupChat.Data,
                UserGetDto = user.Data
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PostGroupChat chat)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ResponseBody<PostGroupChat>>("/GroupChats", JsonSerializer.Serialize(chat), token.Token);
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
        public async Task<IActionResult> GetGroupMembersByGroupId(long groupId)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ResponseBody<List<GetGroupMember>>>("/GroupMembers/getMembersById", JsonSerializer.Serialize(groupId), token.Token);
            return View(response.Data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.DeleteDataAsync<ResponseBody<NoContent>>($"/GroupChats/{id}", token.Token);

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
