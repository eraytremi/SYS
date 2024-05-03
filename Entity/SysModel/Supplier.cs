using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SysModel
{
    public class Supplier:BaseEntity<int>
    {
        
        public string Name { get; set; }
    }
}
