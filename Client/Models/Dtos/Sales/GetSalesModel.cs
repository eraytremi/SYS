using Client.Models.Dtos.SalesDetails;

namespace Client.Models.Dtos.Sales
{
    public class GetSalesModel
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public List<GetSalesDetailModel> GetSalesDetails { get; set; }
    }
}
