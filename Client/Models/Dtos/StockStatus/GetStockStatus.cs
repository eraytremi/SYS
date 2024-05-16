namespace Client.Models.Dtos.StockStatus
{
    public class GetStockStatus
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public double Quantity { get; set; }
    }
}
