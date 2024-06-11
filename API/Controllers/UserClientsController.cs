using Business.Abstract;
using Entity.Dtos.User;
using Entity.Dtos.UserClient;
using Infrastructure.Utilities.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserClientsController : BaseController
    {
        private readonly IUserClientService _service;
        private readonly IConfiguration _configuration;
        private readonly IUserClientRoleService _userRoleService;
        public UserClientsController(IUserClientService service, IConfiguration configuration, IUserClientRoleService userRoleService)
        {
            _service = service;
            _configuration = configuration;
            _userRoleService = userRoleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.GetUserClients(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var response = await _service.LoginUserClient(login);
            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.NameId, response.Data.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            };

            var roles = await _userRoleService.GetByIdAsync((int)response.Data.Id, (int)response.Data.Id);
            var roleId = roles.Data.Select(p => p.RoleId).ToList();

            var accessToken = new JwtGenerator(_configuration).CreateAccessToken(claims, roleId);
            response.Data.Token = accessToken.Token;
            return SendResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.AddUser(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }
    }
}
