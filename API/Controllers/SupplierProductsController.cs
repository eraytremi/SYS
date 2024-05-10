using Business.Abstract;
using Entity.Dtos.Category;
using Entity.Dtos.SupplierProduct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierProductsController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly ISupplierProductService _service;

        public SupplierProductsController(IConfiguration configuration, ISupplierProductService service)
        {
            _configuration = configuration;
            _service = service;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.GetSupplierProductAsync(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }


        [HttpPost]
        public async Task<IActionResult> AddSupplierProduct([FromBody] PostSupplierProduct dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.AddSupplierProduct(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSupplierProduct([FromBody]UpdateSupplierProduct dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.UpdateSupplierProductAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery]int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.DeleteSupplierProductAsync(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }
    }
}
