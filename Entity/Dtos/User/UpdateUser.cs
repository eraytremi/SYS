﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.User
{
    public class UpdateUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string? Password { get; set; }
    }
}
