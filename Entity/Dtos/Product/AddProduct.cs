using Entity.SysModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.Product
{
    public class AddProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Count { get; set; }
        public Unit Unit { get; set; }
    }
}
