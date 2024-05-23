namespace Client.Models.Dtos.Product
{
    public class UpdateProduct
    {
        public long Id { get; set; }
        public int WareHouseId { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Unit Unit { get; set; }
    }
}
