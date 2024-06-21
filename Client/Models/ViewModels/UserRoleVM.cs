using Client.Models.Dtos.Role;
using Client.Models.Dtos.User;
using Client.Models.Dtos.UserRole;

namespace Client.Models.ViewModels
{
    public class UserRoleVM
    {
        public List<GetUserRole> UserRole { get; set; }
        public List<UserGetDto> User { get; set; }
        public List<GetRole> Role { get; set; }

    }

}
