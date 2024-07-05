using Entity.Dtos.UserClient;
using Entity.Dtos.UserClientRole;
using Entity.Dtos.UserRole;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserClientRoleService
    {
        Task<ApiResponse<List<GetUserClientRole>>> GetUserClientRolesAsync(long currentUserId);
        Task<ApiResponse<NoData>> AddUserClientRole(PostUserClientRole userRole, long currentUserId);
        Task<ApiResponse<NoData>> DeleteUserClientRole(int id, long currentUserId);
        Task<ApiResponse<NoData>> UpdateUserClientRoleAsync(UpdateUserClientRole updateRole, long currentUserId);
        Task<ApiResponse<List<GetUserClientRole>>> GetByIdAsync(int id, long currentUserId);
    }
}
