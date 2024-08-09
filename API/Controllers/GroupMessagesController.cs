using Business.Abstract;
using Entity.Dtos.GroupMessage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMessagesController : BaseController
    {
        private readonly IGroupMessageService _groupMessageService;

        public GroupMessagesController(IGroupMessageService groupMessageService)
        {
            _groupMessageService = groupMessageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupMessagesByUserId()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _groupMessageService.GetGroupMessagesByUserId(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PostGroupMessage dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _groupMessageService.AddGroupMessage(dto, currentUserId.GetValueOrDefault()); 
            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGroupMessage dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _groupMessageService.UpdateGMessage(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _groupMessageService.DeleteGMessage(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }
    }
}
