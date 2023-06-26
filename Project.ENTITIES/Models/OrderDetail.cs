using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public short Quantity { get; set; }
        public decimal SubTotal { get; set; }


        //Navigation Properties
        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
