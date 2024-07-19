using Business.Abstract;
using Entity.Dtos.Product;
using Entity.Dtos.Supplier;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SuppliersController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly ISupplierService _service;

        public SuppliersController(IConfiguration configuration, ISupplierService service)
        {
            _configuration = configuration;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.GetSupplierAsync(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddSupplier([FromBody] AddSupplier dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.AddSupplierAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSupplier([FromBody] UpdateSupplier dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.UpdateSupplierAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.DeleteSupplierAsync(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }
    }
}
