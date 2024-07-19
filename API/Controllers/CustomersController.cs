using Business.Abstract;
using Entity.Dtos.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly ICustomerService _service;

        public CustomersController(IConfiguration configuration, ICustomerService service)
        {
            _configuration = configuration;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.GetCustomers(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddCustomer dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.AddCustomer(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCustomer dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.UpdateCustomer(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.DeleteCustomer(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }
    }
}
