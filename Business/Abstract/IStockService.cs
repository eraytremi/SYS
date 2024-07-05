using Entity.Dtos.StockStatus;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IStockService
    {
        Task<ApiResponse<List<GetStock>>> GetAllAsync(long currentUserId);
        Task<ApiResponse<NoData>> AddAsync(PostStock postStockStatus, long currentUserId);
        Task<ApiResponse<NoData>> DeleteAsync(int id, long currentUserId);
        Task<ApiResponse<NoData>> UpdateAsync(UpdateStock updateStockStatus, long currentUserId);
        Task<ApiResponse<NoData>> SellProductAsync(PostStock postStockStatus, long currentUserId);



    }
}
