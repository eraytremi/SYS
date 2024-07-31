using Entity.Dtos.GroupMember;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IGroupMemberService
    {
        Task<ApiResponse<List<GetGroupMember>>> GetAsync(long currentUserId);
        Task<ApiResponse<List<GetGroupMember>>> GetMembersByGroupIdAsync(long id,long currentUserId);

        Task<ApiResponse<NoData>> AddAsync(PostGroupMember groupMember, long currentUserId);
        Task<ApiResponse<NoData>> DeleteAsync(long id,long groupId, long currentUserId);
        Task<ApiResponse<NoData>> UpdateAsync(UpdateGroupMember groupMember, long currentUserId);
    }
}
