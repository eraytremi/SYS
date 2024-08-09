using Entity.Dtos.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.PrivateMessage
{
    public class GetPrivateMessage
    {
        public long Id { get; set; }
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
        public string MessageText { get; set; }
        public bool IsSeen { get; set; } 
        public virtual GetUser Sender { get; set; }
        public virtual GetUser Recipient { get; set; }
    }
}
