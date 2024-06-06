namespace Client.Models.Dtos.Product
{
    public class PostProduct
    {
        public int WareHouseId { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public byte[] Picture { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
    }
}
