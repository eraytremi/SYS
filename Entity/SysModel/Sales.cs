using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SysModel
{
    public class Sales:BaseEntity<long>
    {
        public long CustomerId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SalesDetails> SalesDetails { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

    }
}
