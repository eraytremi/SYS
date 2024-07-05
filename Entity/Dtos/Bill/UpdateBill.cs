using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.Bill
{
    public class UpdateBill
    {
        public long Id { get; set; }
        public long SalesId { get; set; }
        public DateTime BillDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAdress { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
