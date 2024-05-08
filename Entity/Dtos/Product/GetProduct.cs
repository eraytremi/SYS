using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.Product
{
    public class GetProduct
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Count { get; set; }
    }
}
