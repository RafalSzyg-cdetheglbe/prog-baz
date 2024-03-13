using BLL.DTOModels;
using BLL.ServiceInterfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_EF.Services
{
    public class ProductGroupService : IProductGroupBLL
    {

        private readonly WebShopContext _context;

        public ProductGroupService(WebShopContext context)
        {
            _context = context;
        }

        public void AddGroup(GroupRequestDTO groupRequest)
        {
            var group = new ProductGroup
            {
                Name = groupRequest.GroupName,
                ParentId = groupRequest.ParentId
            };

            _context.ProductGroups.Add(group);
            _context.SaveChanges();
        }

        public IEnumerable<GroupDTO> GetGroups(int? parentId, string sortBy, bool sortAscending)
        {
            var query = _context.ProductGroups.AsQueryable();

            if (parentId.HasValue)
            {
                query = query.Where(g => g.ParentId == parentId);
            }

            switch (sortBy.ToLower())
            {
                case "name":
                    query = sortAscending ? query.OrderBy(g => g.Name) : query.OrderByDescending(g => g.Name);
                    break;
                default:
                    query = query.OrderBy(g => g.ProductGroupId);
                    break;
            }

            var groups = query.Select(g => new GroupDTO
            {
                GroupId = g.ProductGroupId,
                GroupName = g.Name,
                PartenId = g.ParentId
            }).ToList();

            return groups;
        }
    }
}
