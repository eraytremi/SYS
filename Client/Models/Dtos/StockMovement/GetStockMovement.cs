using Client.Models.Dtos.Product;

namespace Client.Models.Dtos.StockMovement
{
    public class GetStockMovement
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public bool IsEntry { get; set; }
        public double Quantity { get; set; }
        public DateTime Date { get; set; }
        public string SourceDestination { get; set; }
        public GetProduct GetProduct { get; set; }

    }
}
