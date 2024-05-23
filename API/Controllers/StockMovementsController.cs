using Business.Abstract;
using Entity.Dtos.StockMovement;
using Entity.Dtos.WareHouse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockMovementsController : BaseController
    {
        private readonly IStockMovementService _service;

        public StockMovementsController(IStockMovementService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.GetAllAsync(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PostStockMovement dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.AddAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateStockMovement dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.UpdateAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.DeleteAsync(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpGet("waitingStatuses")]
        public async Task<IActionResult> WaitingStatus()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.WaitingStatuses(currentUserId.GetValueOrDefault());
            return SendResponse(response);

        }

        [HttpDelete("ApproveStatus/{id}")]
        public async Task<IActionResult> ApproveStatus([FromRoute]int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.ApproveStatus(id,currentUserId.GetValueOrDefault());
            return SendResponse(response);

        }

        [HttpDelete("RejectStatus/{id}")]
        public async Task<IActionResult> RejectStatus([FromRoute] int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.RejectStatus(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);

        }
    }
}
