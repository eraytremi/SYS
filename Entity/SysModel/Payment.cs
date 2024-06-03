using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SysModel
{
    public class Payment : BaseEntity<long>
    {
        public PaymentType PaymentType { get; set; }
    }
}
