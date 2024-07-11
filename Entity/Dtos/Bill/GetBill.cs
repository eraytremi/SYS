using Entity.Dtos.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.Bill
{
    public class GetBill
    {
        public long Id { get; set; }
        public long SalesId { get; set; }
        public DateTime BillDate { get; set; }
        public GetSales Sales { get; set; }

    }
}
