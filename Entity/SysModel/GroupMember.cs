using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SysModel
{
    public class GroupMember:BaseEntity<int>
    {
        public long GroupId { get; set; }
        public long UserId { get; set; }

        [ForeignKey("GroupId")]
        public virtual GroupChat Group { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
