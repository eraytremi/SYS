using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SysModel
{
    public class Product:BaseEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Count { get; set; }
        public Unit Unit { get; set; }
    }
}
