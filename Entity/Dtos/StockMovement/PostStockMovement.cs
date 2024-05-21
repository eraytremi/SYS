using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.StockMovement
{
    public class PostStockMovement
    {
        public long ProductId { get; set; }
        public bool IsEntry { get; set; }
        public double Quantity { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
    }
}
