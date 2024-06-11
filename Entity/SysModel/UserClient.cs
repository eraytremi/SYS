using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SysModel
{
    public class UserClient:BaseEntity<long>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
