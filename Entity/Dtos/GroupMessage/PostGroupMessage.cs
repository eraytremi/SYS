using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.GroupMessage
{
    
    public class PostGroupMessage
    {
        
        public long? GroupId { get; set; }
        public string MessageText { get; set; }
    }
}
