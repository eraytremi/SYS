using Client.Models.Dtos.Demand;
using Client.Models.Dtos.User;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Client.ApiServices.Interfaces;
using System.Text.Json;
using Client.Models.Dtos.Chat;

namespace Client.Controllers
{
    public class ChatController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public ChatController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            ViewBag.ActivePerson = token.Name;
            var responseProduct = await _httpApiService.GetDataAsync<ResponseBody<List<GetChat>>>("/Chats", token.Token);

            return View(responseProduct.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetUnreadMessages()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            ViewBag.ActivePerson = token.Name;
            var responseProduct = await _httpApiService.GetDataAsync<ResponseBody<List<GetChat>>>("/Chats/unreadMessages", token.Token);
            ViewBag.UnreadMessages = responseProduct.Data;
            return Json(responseProduct.Data);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");

            var obj = new PostChat
            {
                CreatedBy = (int)token.Id,
                MessageText = message,
                UserName = token.Name
            };

            var response = await _httpApiService.PostDataAsync<ResponseBody<PostChat>>("/Chats", JsonSerializer.Serialize(obj), token.Token);
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
