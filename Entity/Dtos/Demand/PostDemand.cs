using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.Demand
{
    public class PostDemand
    {
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public string Description { get; set; }
        public int StatusType { get; set; }

    }
}
