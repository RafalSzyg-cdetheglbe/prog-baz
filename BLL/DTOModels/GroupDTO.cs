﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels
{
    public class GroupDTO
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public bool HasSubgroups { get; set; }
    }
}
