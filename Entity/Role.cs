﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Role:BaseEntity<int>
    {
        public string Name { get; set; }
        public List<UserRole> UserRoles { get; set; }

    }
}
