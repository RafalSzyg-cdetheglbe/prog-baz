using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public ICollection<OrderPosition> OrderPositions { get; set; }
    }
}

