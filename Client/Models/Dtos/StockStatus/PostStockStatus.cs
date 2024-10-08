﻿namespace Client.Models.Dtos.StockStatus
{
    public class PostStockStatus
    {
        public long ProductId { get; set; }
        public double Quantity { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public bool IsEntry { get; set; }
    }
}
