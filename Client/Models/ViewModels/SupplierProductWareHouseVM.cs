using Client.Models.Dtos.Category;
using Client.Models.Dtos.Product;
using Client.Models.Dtos.Supplier;
using Client.Models.Dtos.WareHouse;

namespace Client.Models.ViewModels
{
    public class SupplierProductWareHouseVM
    {
        public List<GetSupplier> GetSuppliers { get; set; }
        public List<GetProduct> GetProducts { get; set; }
        public List<GetWareHouse> GetWareHouses { get; set; }
        public List<GetCategory> GetCategories { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
