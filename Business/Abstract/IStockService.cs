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
        Task<ApiResponse<List<GetStock>>> GetAllAsync(int currentUserId);
        Task<ApiResponse<NoData>> AddAsync(PostStock postStockStatus, int currentUserId);
        Task<ApiResponse<NoData>> DeleteAsync(int id, int currentUserId);
        Task<ApiResponse<NoData>> UpdateAsync(UpdateStock updateStockStatus, int currentUserId);
        Task<ApiResponse<NoData>> SellProductAsync(PostStock postStockStatus, int currentUserId);



    }
}
