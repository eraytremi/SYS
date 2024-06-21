using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SysModel
{
    public class Supplier : BaseEntity<int>
    {     
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
