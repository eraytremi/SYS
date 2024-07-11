using Client.Models.Dtos.Customer;
using Client.Models.Dtos.SalesDetails;

namespace Client.Models.ViewModels
{
    public class SalesCustomerVm
    {
        public List<PostSalesDetailsModel> SalesDetails { get; set; }
        public PostCustomer Customer { get; set; }
    }
}
