using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SysModel
{
    public class SupplierProduct:BaseEntity<int>
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

    }
}
