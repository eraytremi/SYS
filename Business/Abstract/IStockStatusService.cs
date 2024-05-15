using Entity.Dtos.StockStatus;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IStockStatusService
    {
        Task<ApiResponse<List<GetStockStatus>>> GetAllAsync(int currentUserId);
        Task<ApiResponse<NoData>> AddAsync(PostStockStatus postStockStatus, int currentUserId);
        Task<ApiResponse<NoData>> DeleteAsync(int id, int currentUserId);
        Task<ApiResponse<NoData>> UpdateAsync(UpdateStockStatus  updateStockStatus, int currentUserId);
    }
}
