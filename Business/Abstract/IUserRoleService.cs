using Entity.Dtos.Role;
using Entity.Dtos.UserRole;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserRoleService
    {
        Task<ApiResponse<List<GetUserRole>>> GetUserRolesAsync(long currentUserId, string search = null);
        Task<ApiResponse<NoData>> AddUserRole(AddUserRole userRole, long currentUserId);
        Task<ApiResponse<NoData>> DeleteUserRole(int id, long currentUserId);
        Task<ApiResponse<NoData>> UpdateUserRoleAsync(UpdateUserRole updateRole, long currentUserId);
        Task<ApiResponse<List<GetUserRole>>> GetByIdAsync(int id, long currentUserId);
    }
}
