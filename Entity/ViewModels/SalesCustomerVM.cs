using Entity.Dtos.Customer;
using Entity.Dtos.SalesDetails;
using Entity.SysModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModels
{
    public class SalesCustomerVM
    {
        public List<PostSalesDetails> SalesDetails { get; set; }
        public AddCustomer Customer { get; set; }
    }
}
