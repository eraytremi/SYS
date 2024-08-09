using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.GroupMessage
{
    public class UpdateGroupMessage
    {
        public long Id { get; set; }
        public long? GroupId { get; set; }
        public long? SenderId { get; set; }
        public string MessageText { get; set; }
        public bool IsSeen { get; set; }

    }
}
