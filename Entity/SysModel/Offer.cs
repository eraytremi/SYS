using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entity.SysModel
{
    public class Offer:BaseEntity<int>
    {     
        [JsonPropertyName("Isim")]
        public int ProductName { get; set; }
        public double Count { get; set; }
        public string Description { get; set; }
        public Unit Unit { get; set; }
        public Product  Product { get; set; }

    }
}
