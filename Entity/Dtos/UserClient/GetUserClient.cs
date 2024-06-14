using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.UserClient
{
    public class GetUserClient
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }

    }
}
