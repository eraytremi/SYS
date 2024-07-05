using Entity.Dtos.Role;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRoleService
    {
        Task<ApiResponse<List<GetRole>>> GetRolesAsync(long currentUserId);
        Task<ApiResponse<NoData>> AddRole(AddRole role, long currentUserId);
        Task<ApiResponse<NoData>> DeleteRole(int id, long currentUserId);
        Task<ApiResponse<NoData>> UpdateRoleAsync(UpdateRole updateRole, long currentUserId);
    }
}
