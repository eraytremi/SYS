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
        public UsersController(IUserService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
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
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var accessToken = new JwtGenerator(_configuration).CreateAccessToken(claims);
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

    }
}
