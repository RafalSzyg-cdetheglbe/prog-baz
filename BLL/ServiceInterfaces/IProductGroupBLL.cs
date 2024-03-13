using BLL.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceInterfaces
{
    internal interface IProductGroupBLL
    {
        IEnumerable<GroupDTO> GetGroups(int? parentId, string sortBy, bool sortAscending);
        void AddGroup(GroupRequestDTO groupRequest);

    }
}
