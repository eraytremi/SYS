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
    public class StocksController : BaseController
    {
        private readonly IStockService _stockStatusService;
        private readonly IConfiguration _configuration;
        public StocksController(IStockService stockStatusService, IConfiguration configuration)
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

        [HttpGet("getByIdProductId")]
        public async Task<IActionResult> Get(long productId)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _stockStatusService.GetByProductIdAsync(productId,currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PostStock dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _stockStatusService.AddAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateStock dto)
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
