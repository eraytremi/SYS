using Autofac.Core;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Repositories.Abstract;
using Entity;
using Entity.SysModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : BaseController
    {
        private readonly  IChatService _chatService;
        
        public ChatsController(IChatService chatService)
        {
            _chatService = chatService;
        
        }
        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            var currentUserId = CurrentUser.Get(HttpContext);

            var response = await _chatService.GetMessages((long)currentUserId);
            return SendResponse(response);
        }

        [HttpGet("unreadMessages")]
        public async Task<IActionResult> GetUnreadMessages()
        {
            var currentUserId = CurrentUser.Get(HttpContext);

            var response = await _chatService.GetUnreadMessages((long)currentUserId);
            return SendResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> Chat([FromQuery]string user,long senderId,string message)
        {
            //var currentUserId = CurrentUser.Get(HttpContext);

            await _chatService.SendMessage(user,senderId, message);
            return Ok();
        }
    }
}
