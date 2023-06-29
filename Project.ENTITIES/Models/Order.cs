using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Order : BaseEntity
    {
        public int AddressId { get; set; }
        public string? AppUserId { get; set; }
        public decimal TotalPrice { get; set; }


        //Navigation Properties
        public Address Address { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
