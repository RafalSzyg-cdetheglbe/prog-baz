using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Image { get; set; }
        public bool IsActive { get; set; }
        public int? GroupId { get; set; }
        public ProductGroup? Group { get; set; }
        public ICollection<BasketPosition>? BasketPositions { get; set; }
    }
}
