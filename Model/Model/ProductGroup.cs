using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class ProductGroup
    {
        public int ProductGroupId { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public ProductGroup? Parent { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
