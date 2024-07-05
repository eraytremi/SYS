using Entity.Dtos.Category;
using Entity.Dtos.WareHouse;
using Entity.SysModel;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IWareHouseService
    {
        Task<ApiResponse<List<GetWareHouse>>> GetListWareHouseAsync(long currentUserId);
        Task<ApiResponse<NoData>> AddWareHouse(PostWareHouse postWareHouse, long currentUserId);
        Task<ApiResponse<NoData>> DeleteWareHouse(int id, long currentUserId);
        Task<ApiResponse<NoData>> UpdateWareHouseAsync(UpdateWareHouse updateWareHouse, long currentUserId);
    }
}
