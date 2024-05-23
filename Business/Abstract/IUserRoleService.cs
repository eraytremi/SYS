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
        Task<ApiResponse<List<GetUserRole>>> GetUserRolesAsync(int currentUserId);
        Task<ApiResponse<NoData>> AddUserRole(AddUserRole userRole, int currentUserId);
        Task<ApiResponse<NoData>> DeleteUserRole(int id, int currentUserId);
        Task<ApiResponse<NoData>> UpdateUserRoleAsync(UpdateUserRole updateRole, int currentUserId);
        Task<ApiResponse<List<GetUserRole>>> GetByIdAsync(int id, int currentUserId);
    }
}
