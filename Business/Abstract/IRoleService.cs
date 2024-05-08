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
        Task<ApiResponse<List<GetRole>>> GetRolesAsync(int currentUserId);
        Task<ApiResponse<NoData>> AddRole(AddRole role, int currentUserId);
        Task<ApiResponse<NoData>> DeleteRole(int id, int currentUserId);
        Task<ApiResponse<NoData>> UpdateRoleAsync(UpdateRole updateRole, int currentUserId);
    }
}
