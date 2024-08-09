using Entity.Dtos.PrivateMessage;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPrivateMessageService
    {
        Task<ApiResponse<List<GetPrivateMessage>>> GetPrivateMessages(long userId,long recipientId,long currentUserId);
        Task<ApiResponse<NoData>> AddPrivateMessage(PostPrivateMessage message, long currentUserId);
        Task<ApiResponse<NoData>> DeletePMessage(long id,long userId, long currentUserId);
        Task<ApiResponse<NoData>> UpdatePMessage(UpdatePrivateMessage message, long currentUserId);
    }
}
