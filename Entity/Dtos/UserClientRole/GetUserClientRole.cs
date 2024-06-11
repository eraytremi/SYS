using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.UserClientRole
{
    public class GetUserClientRole
    {
        public int Id { get; set; }
        public long UserClientId { get; set; }
        public int RoleId { get; set; }
    }
}
