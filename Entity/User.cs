using Entity.SysModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class User:BaseEntity<long>
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public virtual List<PrivateMessage> SentMessages { get; set; } = new List<PrivateMessage>();
        public virtual List<PrivateMessage> ReceivedMessages { get; set; } = new List<PrivateMessage>();
        public virtual List<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();
        public List<UserRole> UserRoles { get; set; }
    }
}
