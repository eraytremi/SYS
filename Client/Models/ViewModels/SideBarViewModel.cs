using Client.Models.Dtos.Chat;
using Client.Models.Dtos.User;

namespace Client.Models.ViewModels
{
    public class SideBarViewModel
    {
        public UserGetDto UserGetDto { get; set; }
        public string RoleId { get; set; }
        public List<GetChat> GetChats { get; set; }
    }
}
