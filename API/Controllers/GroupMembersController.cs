using Business.Abstract;
using Business.Concrete;
using Entity.Dtos.GroupChat;
using Entity.Dtos.GroupMember;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMembersController : BaseController
    {
        private readonly IGroupMemberService _groupMemberService;

        public GroupMembersController(IGroupMemberService groupMemberService)
        {
            _groupMemberService = groupMemberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _groupMemberService.GetAsync(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPost("getMembersById")]
        public async Task<IActionResult> GetMembersById([FromBody]long id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _groupMemberService.GetMembersByGroupIdAsync(id,currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PostGroupMember dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _groupMemberService.AddAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGroupMember dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _groupMemberService.UpdateAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id,long groupId)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _groupMemberService.DeleteAsync(id,groupId,currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }
    }

}
