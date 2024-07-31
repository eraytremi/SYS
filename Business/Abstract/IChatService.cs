using Entity.Dtos.Message;
using Entity.SysModel;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IChatService
    {
        Task<ApiResponse<List<Message>>> GetMessages(long currentUserId);
        Task<ApiResponse<List<UnreadMessage>>> GetUnreadMessages(long currentUserId);

        Task SendMessage(string user,long senderId, string message);
    }
}
