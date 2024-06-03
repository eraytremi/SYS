using Client.Models.Dtos.Product;
using Client.Models.Dtos.StockStatus;

namespace Client.Models.ViewModels
{
    public class ProductStockVm
    {
        public List<GetProduct> GetProducts { get; set; }
        public List<GetStockStatus>  GetStockStatuses { get; set; }


    }
}
