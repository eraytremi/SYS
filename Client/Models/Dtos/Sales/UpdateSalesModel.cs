namespace Client.Models.Dtos.Sales
{
    public class UpdateSalesModel
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
