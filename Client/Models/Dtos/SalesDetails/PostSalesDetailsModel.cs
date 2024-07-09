namespace Client.Models.Dtos.SalesDetails
{
    public class PostSalesDetailsModel
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
