using Entity.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.Message
{
    public class AllUserMessage
    {
        public int Sender { get; set; } //gönderen kişi
        public string MessageText { get; set; }
        public DateTime Date { get; set; }
        public GetUser SenderPerson { get; set; }
    }

}
