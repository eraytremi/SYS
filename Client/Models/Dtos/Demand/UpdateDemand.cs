namespace Client.Models.Dtos.Demand
{
    public class UpdateDemand
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public string Description { get; set; }
        public int StatusType { get; set; }

    }
}
