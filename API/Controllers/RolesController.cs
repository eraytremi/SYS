using Autofac.Core;
using Business.Abstract;
using Entity.Dtos.Role;
using Entity.Dtos.User;
using Infrastructure.Utilities.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IRoleService _service;

        public RolesController(IRoleService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.GetRolesAsync(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }
       

        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] AddRole dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.AddRole(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody]UpdateRole dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.UpdateRoleAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole([FromQuery]int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.DeleteRole(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }
    }
}
