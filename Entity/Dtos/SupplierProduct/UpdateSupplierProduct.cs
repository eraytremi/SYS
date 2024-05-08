using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.SupplierProduct
{
    public class UpdateSupplierProduct
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }

    }
}
