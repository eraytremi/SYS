using Business.Abstract;
using Entity.Dtos.UserClientRole;
using Entity.Dtos.UserRole;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserClientRolesController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IUserClientRoleService _service;

        public UserClientRolesController(IUserClientRoleService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.GetUserClientRolesAsync(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }


        [HttpPost]
        public async Task<IActionResult> AddUserRole([FromBody] PostUserClientRole dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.AddUserClientRole(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserClientRole dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.UpdateUserClientRoleAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole([FromQuery] int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.DeleteUserClientRole(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }
    }
}
