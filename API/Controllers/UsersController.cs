using Business.Abstract;
using Entity.Dtos.User;
using Infrastructure.Utilities.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _service;
        private readonly IConfiguration _configuration;
        private readonly IUserRoleService _userRoleService;
        public UsersController(IUserService service, IConfiguration configuration, IUserRoleService userRoleService)
        {
            _service = service;
            _configuration = configuration;
            _userRoleService = userRoleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string search = null)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.GetUsers(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }   

        [HttpPost("login")]
        public async Task<IActionResult> Login ([FromBody]LoginDto loginDto)
        {           
            var response = await _service.LoginUser(loginDto);
            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.NameId, response.Data.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                
            };

            var roles = await _userRoleService.GetByIdAsync((int)response.Data.Id, response.Data.Id);
            var roleId = roles.Data.Select(p => p.RoleId).ToList();

            var accessToken = new JwtGenerator(_configuration).CreateAccessToken(claims,roleId);
            response.Data.Token = accessToken.Token;
            return SendResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterDto dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response =  await _service.AddUser(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUser dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.UpdateUser(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.DeleteUser(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }
    }
}
