namespace Client.Models.Dtos.StockStatus
{
    public class PostStockStatus
    {
        public long ProductId { get; set; }
        public int WareHouseId { get; set; }
        public double Quantity { get; set; }
    }
}
