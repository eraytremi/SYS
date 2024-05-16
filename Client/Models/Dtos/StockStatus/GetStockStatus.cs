using Client.Models.Dtos.Product;

namespace Client.Models.Dtos.StockStatus
{
    public class GetStockStatus
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public double Quantity { get; set; }
        public GetProduct GetProduct { get; set; }
    }
}
