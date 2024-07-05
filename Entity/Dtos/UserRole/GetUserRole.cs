using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.UserRole
{
    public class GetUserRole
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Mail { get; set; }
    }
}
