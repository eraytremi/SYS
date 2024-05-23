using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.SysModel
{
    public class StockMovement:BaseEntity<long>
    {
        public long ProductId { get; set; } 
        public bool IsEntry { get; set; }
        public double Quantity { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public StatusType StatusType { get; set; }
        public Product Product { get; set; }
        
    } 
}
