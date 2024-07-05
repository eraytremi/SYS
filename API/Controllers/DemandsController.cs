using Autofac.Core;
using Business.Abstract;
using Entity.Dtos.Category;
using Entity.Dtos.Demand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandsController : BaseController
    {
        private readonly IDemandService _demandService;

        public DemandsController(IDemandService demandService)
        {
            _demandService = demandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _demandService.GetDemandAsync(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PostDemand dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _demandService.AddDemandAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateDemand dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _demandService.UpdateDemandAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _demandService.DeleteDemandAsync(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        //bekleyen talepleri listeler
        [HttpGet("waitingDemands")]
        public async Task<IActionResult> WaitingStatus()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _demandService.WaitingDemands(currentUserId.GetValueOrDefault());
            return SendResponse(response);

        }

        //kabul edilmiş talepleri listeler
        [HttpGet("approvedDemands")]
        public async Task<IActionResult> ApprovedDemands()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _demandService.ApprovedDemands(currentUserId.GetValueOrDefault());
            return SendResponse(response);

        }



        //red edilmiş talepleri listeler
        [HttpGet("rejectedDemands")]
        public async Task<IActionResult> RejectedDemands()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _demandService.RejectedDemands(currentUserId.GetValueOrDefault());
            return SendResponse(response);

        }


        //talepleri onaylar
        [HttpDelete("approveDemand/{id}")]
        public async Task<IActionResult> ApproveDemand([FromRoute] int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _demandService.ApproveDemand(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);

        }

        //talepleri red eder
        [HttpDelete("rejectDemand/{id}")]
        public async Task<IActionResult> RejectDemand([FromRoute] int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _demandService.RejectDemand(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);

        }
    }
}
