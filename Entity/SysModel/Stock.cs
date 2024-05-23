using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SysModel
{
    public class Stock:BaseEntity<long>
    {
        public long ProductId { get; set; }
        public double Quantity { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
