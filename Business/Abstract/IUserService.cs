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
        Task<ApiResponse<NoData>> AddUser(RegisterDto dto,int currentUserId);
        Task<ApiResponse<List<GetUser>>> GetUsers(int currentUserId);
        Task<ApiResponse<NoData>> UpdateUser(UpdateUser dto, int currentUserId);
        Task<ApiResponse<NoData>> DeleteUser(int id, int currentUserId);
        Task<ApiResponse<GetUser>> LoginUser(LoginDto dto);
    }
}
