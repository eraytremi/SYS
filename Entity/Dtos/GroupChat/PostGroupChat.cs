using Entity.Dtos.GroupMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.GroupChat
{
    public class PostGroupChat
    {
        public long GroupId { get; set; }
        public string GroupName { get; set; }
        public List<PostGroupMember> GroupMembers { get; set; }
    }
}
