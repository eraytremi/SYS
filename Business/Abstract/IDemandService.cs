using Entity.Dtos.Demand;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDemandService
    {
        Task<ApiResponse<List<GetDemand>>> GetDemandAsync(long currentUserId);
        Task<ApiResponse<NoData>> AddDemandAsync(PostDemand demand, long currentUserId);
        Task<ApiResponse<NoData>> DeleteDemandAsync(int id, long currentUserId);
        Task<ApiResponse<NoData>> UpdateDemandAsync(UpdateDemand demand, long currentUserId);
        Task<ApiResponse<List<GetDemand>>> WaitingDemands(long currentUserId);
        Task<ApiResponse<List<GetDemand>>> ApprovedDemands(long currentUserId);
        Task<ApiResponse<List<GetDemand>>> RejectedDemands(long currentUserId);
        Task<ApiResponse<NoData>> ApproveDemand(int id,long currentUserId);
        Task<ApiResponse<NoData>> RejectDemand(int id, long currentUserId);

    }
}
