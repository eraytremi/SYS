using Business.Abstract;
using Business.Concrete;
using Entity.Dtos.GroupChat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupChatsController : BaseController
    {
        private readonly IGroupChatService _groupChatService;

        public GroupChatsController(IGroupChatService groupChatService)
        {
            _groupChatService = groupChatService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupChat(long groupId)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _groupChatService.GetGroupChat(groupId,currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpGet("getGroupChats")]
        public async Task<IActionResult> GetGroupChats()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _groupChatService.GetGroupChats(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpGet("GetGroupChatByUserId")]
        public async Task<IActionResult> GetGroupChatByUserId()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _groupChatService.GetGroupChatById(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PostGroupChat dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _groupChatService.AddGroupChat(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGroupChat dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _groupChatService.UpdateGroupChat(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _groupChatService.DeleteGroupChat(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }
    }
}
