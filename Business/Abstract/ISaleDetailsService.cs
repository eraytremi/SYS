using Entity.Dtos.Sales;
using Entity.Dtos.SalesDetails;
using Entity.ViewModels;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISaleDetailsService
    {
        Task<ApiResponse<List<GetSalesDetails>>> GetSalesDetailsAsync(long currentUserId);
        Task<ApiResponse<string>> AddSalesDetailsAsync(SalesCustomerVM vm, long currentUserId);
        Task<ApiResponse<NoData>> DeleteSalesDetailsAsync(long id, long currentUserId);
        Task<ApiResponse<NoData>> UpdateSalesDetailsAsync(UpdateSalesDetails update, long currentUserId);
    }
}
