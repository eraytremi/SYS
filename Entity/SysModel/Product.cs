using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SysModel
{
    public class Product:BaseEntity<long>
    {
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public int WareHouseId { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? Barcode { get; set; }
        public Unit Unit { get; set; } 
        public List<Supplier> Supplier { get; set; }  
        public Category Category { get; set; }
        public WareHouse WareHouse { get; set; }
    }
}
