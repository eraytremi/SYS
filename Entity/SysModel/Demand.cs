using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SysModel
{
    public class Demand : BaseEntity<int>
    {
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public string Description { get; set; }
        public StatusType StatusType { get; set; }
    }
}
