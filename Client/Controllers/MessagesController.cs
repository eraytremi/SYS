using Client.Models.Dtos.User;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Client.ApiServices.Interfaces;
using Client.Models.Dtos.GroupChat;
using System.Text.Json;
using Client.Models.Dtos.GroupMessages;

namespace Client.Controllers
{
    public class MessagesController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public MessagesController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            ViewData["user"] = token.Id; 
            var groupChat = await _httpApiService.GetDataAsync<ResponseBody<List<GetGroupChat>>>("/GroupChats/GetGroupChatByUserId", token.Token);

            return View(groupChat.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostGroupMessage chat)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ResponseBody<PostGroupMessage>>("/GroupMessages", JsonSerializer.Serialize(chat), token.Token);
            if (response.StatusCode == 201)
            {
                return Json(new { IsSuccess = true, Message = "Başarıyla Kaydedildi", response.Data });
            }
            else
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });
            }
        }

    }
}
