using Client.Models.Dtos.GroupMember;

namespace Client.Models.Dtos.GroupChat
{
    public class PostGroupChat
    {
        public long GroupId { get; set; }
        public string GroupName { get; set; }
        public List<PostGroupMember> GroupMembers { get; set; }
    }
}
