using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.UserRole
{
    public class UpdateUserRole
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public long UserId { get; set; }
    }
}
