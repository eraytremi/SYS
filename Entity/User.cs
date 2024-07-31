﻿using Entity.SysModel;
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
        public virtual ICollection<Message> SentMessages { get; set; } 
        public virtual ICollection<Message> ReceivedMessages { get; set; } 
        public virtual ICollection<GroupMember> GroupMembers { get; set; } 
        public List<UserRole> UserRoles { get; set; }
    }
}
