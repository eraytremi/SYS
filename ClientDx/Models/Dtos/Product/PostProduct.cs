namespace ClientDvx.Models.Dtos.Product
{
    public class PostProduct
    {
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public string SeriNo { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Count { get; set; }
        public Unit Unit { get; set; }
    }
}
