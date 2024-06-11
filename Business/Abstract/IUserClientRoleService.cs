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
        Task<ApiResponse<List<GetUserClientRole>>> GetUserClientRolesAsync(int currentUserId);
        Task<ApiResponse<NoData>> AddUserClientRole(PostUserClientRole userRole, int currentUserId);
        Task<ApiResponse<NoData>> DeleteUserClientRole(int id, int currentUserId);
        Task<ApiResponse<NoData>> UpdateUserClientRoleAsync(UpdateUserClientRole updateRole, int currentUserId);
        Task<ApiResponse<List<GetUserClientRole>>> GetByIdAsync(int id, int currentUserId);
    }
}
