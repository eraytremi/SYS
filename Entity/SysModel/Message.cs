using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SysModel
{
    public class Message:BaseEntity<long>
    {

        public long Reciever { get; set; } //alıcı kişi
        public long Sender { get; set; } //gönderen kişi
        public string MessageText { get; set; }
        public bool IsSeen { get; set; }
    }
}
