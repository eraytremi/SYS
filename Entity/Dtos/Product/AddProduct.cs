using Entity.SysModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.Product
{
    public class AddProduct
    {
        public int WareHouseId { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
    }
}
