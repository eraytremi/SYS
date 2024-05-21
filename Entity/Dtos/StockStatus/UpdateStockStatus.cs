using Entity.SysModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.StockStatus
{
    public class UpdateStockStatus
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public double Quantity { get; set; }
        

    }
}
