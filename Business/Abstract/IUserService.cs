using Entity.Dtos.User;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<ApiResponse<NoData>> AddUser(RegisterDto dto,long currentUserId);
        Task<ApiResponse<List<GetUser>>> GetUsers(long currentUserId);
        Task<ApiResponse<NoData>> UpdateUser(UpdateUser dto, long currentUserId);
        Task<ApiResponse<NoData>> DeleteUser(int id, long currentUserId);
        Task<ApiResponse<GetUser>> LoginUser(LoginDto dto);
    }
}
