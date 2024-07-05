using Entity.Dtos.Bill;
using Entity.Dtos.Sales;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBillService
    {
        Task<ApiResponse<List<GetBill>>> GetBillAsync(long currentUserId);
        Task<ApiResponse<NoData>> AddBillAsync(PostBill bill, long currentUserId);
        Task<ApiResponse<NoData>> DeleteBillAsync(long id, long currentUserId);
        Task<ApiResponse<NoData>> UpdateBillAsync(UpdateBill bill, long currentUserId);
    }
}
