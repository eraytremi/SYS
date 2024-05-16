using Client.Models.Dtos.Category;
using Client.Models.Dtos.Supplier;
using Client.Models.Dtos.WareHouse;

namespace Client.Models.Dtos.Product
{
    public class GetProduct
    {
        public long Id { get; set; }
        public int WareHouseId { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public GetCategory GetCategory { get; set; }
        public GetSupplier GetSupplier { get; set; }
        public GetWareHouse GetWareHouse { get; set; }
    }
}
