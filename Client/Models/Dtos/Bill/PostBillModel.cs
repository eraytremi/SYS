namespace Client.Models.Dtos.Bill
{
    public class PostBillModel
    {
        public long SalesId { get; set; }
        public DateTime BillDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAdress { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
