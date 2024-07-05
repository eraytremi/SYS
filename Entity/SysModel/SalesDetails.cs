using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SysModel
{
    public class SalesDetails :BaseEntity<long>
    {
        public long SalesId { get; set; }  
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("SalesId")]
        public Sales Sales { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

    }
}
