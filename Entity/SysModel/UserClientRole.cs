using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SysModel
{
    public class UserClientRole:BaseEntity<int>
    {
        public long UserClientId { get; set; }
        public int RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }
        [ForeignKey(nameof(UserClientId))]
        public UserClient UserClient { get; set; }
    }
}
