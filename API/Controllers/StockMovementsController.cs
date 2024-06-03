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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.DeleteAsync(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        //bekleyen onayları listeler
        [HttpGet("waitingStatuses")]
        public async Task<IActionResult> WaitingStatus()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.WaitingStatuses(currentUserId.GetValueOrDefault());
            return SendResponse(response);

        }

        //kabul edilmiş onayları listeler
        [HttpGet("approvedStatuses")]
        public async Task<IActionResult> ApprovedStatuses()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.AprrovedStatuses(currentUserId.GetValueOrDefault());
            return SendResponse(response);

        }



        //red edilmiş onayları listeler
        [HttpGet("rejectedStatuses")]
        public async Task<IActionResult> RejectedStatuses()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.RejectedStatuses(currentUserId.GetValueOrDefault());
            return SendResponse(response);

        }


        //status'u onaylar
        [HttpDelete("approvestatus/{id}")]
        public async Task<IActionResult> ApproveStatus([FromRoute]int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.ApproveStatus(id,currentUserId.GetValueOrDefault());
            return SendResponse(response);

        }

        //status'u red eder
        [HttpDelete("rejectstatus/{id}")]
        public async Task<IActionResult> RejectStatus([FromRoute] int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.RejectStatus(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);

        }
    }
}
