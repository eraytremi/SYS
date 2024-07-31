using Entity.Dtos.GroupChat;
using Entity.SysModel;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IGroupChatService
    {
        Task<ApiResponse<NoData>> AddGroupChat(PostGroupChat dto, long currentUserId);
        Task<ApiResponse<List<GetGroupChat>>> GetGroupChat(long currentUserId);
        Task<ApiResponse<NoData>> UpdateGroupChat(UpdateGroupChat dto, long currentUserId);
        Task<ApiResponse<NoData>> DeleteGroupChat(long id, long currentUserId);

    }
}
