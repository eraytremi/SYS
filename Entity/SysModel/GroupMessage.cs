﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SysModel
{
    public class GroupMessage:BaseEntity<long>
    {
        public long? GroupId { get; set; }
        public long? SenderId { get; set; }
        public string MessageText { get; set; }
        public bool IsSeen { get; set; }
        [ForeignKey("GroupId")]
        public virtual GroupChat Group { get; set; }
        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; }

    }
}
