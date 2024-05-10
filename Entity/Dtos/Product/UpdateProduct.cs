using Entity.SysModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.Product
{
    public class UpdateProduct
    {
        public long Id { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public string? SeriNo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Count { get; set; }
        public decimal Price { get; set; }
        public Unit Unit { get; set; }
    }
}
