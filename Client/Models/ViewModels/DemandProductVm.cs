using Client.Models.Dtos.Demand;
using Client.Models.Dtos.Product;

namespace Client.Models.ViewModels
{
    public class DemandProductVm
    {
        public List<GetDemand> Demand { get; set; }
        public List<GetProduct> Product { get; set; }

    }
}
