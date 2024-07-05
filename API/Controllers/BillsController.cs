using Business.Abstract;
using Entity.Dtos.Bill;
using Entity.Dtos.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : BaseController
    {
        private readonly IBillService _service;
        private readonly IConfiguration _configuration;
        public BillsController(IBillService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.GetBillAsync(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PostBill dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.AddBillAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBill dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.UpdateBillAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.DeleteBillAsync(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }
    }
}
