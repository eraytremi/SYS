using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.Email
{
    public class EmailGetDto
    {
        public string SenderMail { get; set; }
        public string SenderPass { get; set; }
        public int Port { get; set; } = 587;
        public string Host { get; set; } = "smtp.outlook.com";
        public bool EnableSsl { get; set; } = true;
    }
}
