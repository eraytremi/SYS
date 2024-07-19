using Autofac.Core;
using Business.Abstract;
using Business.Concrete;
using Entity.Dtos.Category;
using Entity.Dtos.SalesDetails;
using Entity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesDetailsController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly ISaleDetailsService _service;

        public SalesDetailsController(IConfiguration configuration, ISaleDetailsService service)
        {
            _configuration = configuration;
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.GetSalesDetailsAsync(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SalesCustomerVM vm)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.AddSalesDetailsAsync(vm, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSalesDetails dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.UpdateSalesDetailsAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.DeleteSalesDetailsAsync(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }
    }
}
