using Entity.Dtos.StockMovement;
using Entity.Dtos.StockStatus;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IStockMovementService
    {
        Task<ApiResponse<List<GetStockMovement>>> GetAllAsync(long currentUserId);
        Task<ApiResponse<NoData>> AddAsync(PostStockMovement postStockMovement, long currentUserId);
        Task<ApiResponse<NoData>> DeleteAsync(int id, long currentUserId);
        Task<ApiResponse<NoData>> UpdateAsync(UpdateStockMovement updateStockMovement, long currentUserId);
        Task<ApiResponse<NoData>> RejectStatus(int id,long currentUserId);
        Task<ApiResponse<NoData>> ApproveStatus(int id, long currentUserId);
        Task<ApiResponse<List<GetStockMovement>>> AprrovedStatuses(long currentUserId);
        Task<ApiResponse<List<GetStockMovement>>> RejectedStatuses(long currentUserId);
        Task<ApiResponse<List<GetStockMovement>>> WaitingStatuses(long currentUserId);


    }
}
