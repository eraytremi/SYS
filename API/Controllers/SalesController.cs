using Autofac.Core;
using Business.Abstract;
using Entity.Dtos.Category;
using Entity.Dtos.Sales;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly ISaleService _saleService;
        public SalesController(IConfiguration configuration, ISaleService saleService)
        {
            _configuration = configuration;
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _saleService.GetSalesAsync(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpGet("getSale")]
        public async Task<IActionResult> Get(long salesId)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _saleService.GetSales(salesId,currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PostSales dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _saleService.AddSalesAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSales dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _saleService.UpdateSalesAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _saleService.DeleteSalesAsync(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }
    }
}

