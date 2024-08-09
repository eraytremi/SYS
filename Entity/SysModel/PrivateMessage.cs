using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SysModel
{
    public class PrivateMessage:BaseEntity<long>
    {
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
        public string MessageText { get; set; }
        public bool IsSeen { get; set; }
    
        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; }
        [ForeignKey("RecipientId")]
        public virtual User Recipient { get; set; }
    }
}
