using Entity.Dtos.GroupMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.GroupChat
{
    public class GetGroupChat
    {
        public long Id { get; set; }
        public string GroupName { get; set; }
        public List<GetGroupMember>  GetGroupMembers { get; set; }
    }
}
