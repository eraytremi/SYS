﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.Customer
{
    public class UpdateCustomer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string lastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
    }
}