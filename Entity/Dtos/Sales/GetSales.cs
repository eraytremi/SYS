using Entity.Dtos.SalesDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.Sales
{
    public class GetSales
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public List<GetSalesDetails> GetSalesDetails { get; set; }
    }
}
