using Client.Models.Dtos.User;
using Client.Models.Dtos.UserRole;

namespace Client.Models.Dtos.GroupMember
{
    public class GetGroupMember
    {
        public long Id { get; set; }
        public long GroupId { get; set; }
        public long UserId { get; set; }
        public UserGetDto GetUser { get; set; }
    }
}
