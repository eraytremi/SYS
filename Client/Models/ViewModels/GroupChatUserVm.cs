using Client.Models.Dtos.GroupChat;
using Client.Models.Dtos.User;
using Client.Models.Dtos.UserRole;

namespace Client.Models.ViewModels
{
    public class GroupChatUserVm
    {
        public List<GetGroupChat> GetGroupChat { get; set; }
        public List<UserGetDto> UserGetDto { get; set; }
    }
}
