using Client.Models.Dtos.GroupMember;
using Client.Models.Dtos.GroupMessages;

namespace Client.Models.Dtos.GroupChat
{
    public class GetGroupChat
    {
        public long Id { get; set; }
        public string GroupName { get; set; }
        public List<GetGroupMember> GetGroupMembers { get; set; }
        public List<GetGroupMessage> GetGroupMessages { get; set; }
    }
}
