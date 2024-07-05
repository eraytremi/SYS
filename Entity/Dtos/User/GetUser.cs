using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.User
{
    public class GetUser
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Mail { get; set; }
        public string Token { get; set; }
    }
}
