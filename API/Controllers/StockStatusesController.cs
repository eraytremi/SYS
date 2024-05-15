using Autofac.Core;
using Business.Abstract;
using Entity.Dtos.StockStatus;
using Entity.Dtos.WareHouse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockStatusesController : BaseController
    {
        private readonly IStockStatusService _stockStatusService;
        private readonly IConfiguration _configuration;
        public StockStatusesController(IStockStatusService stockStatusService, IConfiguration configuration)
        {
            _stockStatusService = stockStatusService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _stockStatusService.GetAllAsync(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PostStockStatus dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _stockStatusService.AddAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateStockStatus dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _stockStatusService.UpdateAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _stockStatusService.DeleteAsync(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }
    }
}
