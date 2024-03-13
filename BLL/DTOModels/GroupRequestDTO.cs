using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels
{
    public class GroupRequestDTO
    {
        public string GroupName { get; set; }
        public int? ParentId { get; set; }
    }
}
