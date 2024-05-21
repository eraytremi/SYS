using Entity.SysModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.StockStatus
{
    public class PostStockStatus
    {
        public long ProductId { get; set; }
        public double Quantity { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        
    }
}
