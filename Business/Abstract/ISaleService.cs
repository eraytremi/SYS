using Entity.Dtos.Product;
using Entity.Dtos.Sales;
using Entity.SysModel;
using Infrastructure.Utilities.Responses;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISaleService
    {
        Task<ApiResponse<List<GetSales>>> GetSalesAsync(long currentUserId);
        Task<ApiResponse<NoData>> AddSalesAsync(PostSales sales, long currentUserId);
        Task<ApiResponse<NoData>> DeleteSalesAsync(long id, long currentUserId);
        Task<ApiResponse<NoData>> UpdateSalesAsync(UpdateSales update, long currentUserId);
        Task<ApiResponse<GetSales>> GetSales(long salesId, long currentUserId);
    }
    
}
