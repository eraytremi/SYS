namespace Client.Models.Dtos.StockStatus
{
    public class UpdateStockStatus
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public int WareHouseId { get; set; }
        public double Quantity { get; set; }
    }
}
