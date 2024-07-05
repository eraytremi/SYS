using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SysModel
{
    public class Sales:BaseEntity<long>
    {
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SalesDetails> SalesDetails { get; set; }
        public Customer Customer { get; set; }

    }
}
