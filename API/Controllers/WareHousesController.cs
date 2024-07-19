using Business.Abstract;
using Entity.Dtos.Category;
using Entity.Dtos.WareHouse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WareHousesController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IWareHouseService _service;

        public WareHousesController(IConfiguration configuration, IWareHouseService service)
        {
            _configuration = configuration;
            _service = service;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.GetListWareHouseAsync(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PostWareHouse dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.AddWareHouse(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateWareHouse dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.UpdateWareHouseAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete( int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.DeleteWareHouse(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }
    }
}
