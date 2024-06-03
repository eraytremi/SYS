using Business.Abstract;
using Entity.Dtos.Role;
using Entity.Dtos.UserRole;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRoleService _service;

        public UserRolesController(IUserRoleService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.GetUserRolesAsync(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }


        [HttpPost]
        public async Task<IActionResult> AddUserRole([FromBody]AddUserRole dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.AddUserRole(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRole dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.UpdateUserRoleAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole([FromQuery]int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.DeleteUserRole(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }
    }
}
