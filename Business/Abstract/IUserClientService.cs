using Entity.Dtos.User;
using Entity.Dtos.UserClient;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserClientService
    {
        Task<ApiResponse<NoData>> AddUserClient(PostUserClient dto, int currentUserId);
        Task<ApiResponse<List<GetUserClient>>> GetUserClients(int currentUserId);
        Task<ApiResponse<NoData>> UpdateUserClient(UpdateUserClient dto, int currentUserId);
        Task<ApiResponse<NoData>> DeleteUserClient(int id, int currentUserId);
        Task<ApiResponse<GetUserClient>> LoginUserClient(Login dto);
    }
}
