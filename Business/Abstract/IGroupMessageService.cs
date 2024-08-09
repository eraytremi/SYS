using Entity.Dtos.GroupMessage;
using Entity.Dtos.PrivateMessage;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IGroupMessageService
    {
        Task<ApiResponse<List<GetGroupMessage>>> GetGroupMessagesByUserId(long currentUserId);
        Task<ApiResponse<NoData>> AddGroupMessage(PostGroupMessage message, long currentUserId);
        Task<ApiResponse<NoData>> DeleteGMessage(long id, long currentUserId);
        Task<ApiResponse<NoData>> UpdateGMessage(UpdateGroupMessage message, long currentUserId);
    }
}
