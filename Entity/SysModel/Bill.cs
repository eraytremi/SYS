using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SysModel
{
    public class Bill:BaseEntity<long>
    {
        public long SalesId { get; set; }
        public DateTime BillDate { get; set; }
        [ForeignKey("SalesId")]
        public Sales Sales { get; set; }
    }
}
