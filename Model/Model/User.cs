using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public enum UserType
    {
        Admin,
        Casual
    }

    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
        public bool IsActive { get; set; }
        public int? GroupId { get; set; }
        public UserGroup? Group { get; set; }
        public ICollection<BasketPosition> BasketPositions { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
