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
        public int WareHouseId { get; set; }

        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public string SeriNo { get; set; }     
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Count { get; set; }
        public Unit Unit { get; set; }
    }
}
