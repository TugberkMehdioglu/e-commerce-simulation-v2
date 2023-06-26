using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class ProductAttribute : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
        public int ProductId { get; set; }

        //Navigation Properties
        public Product Product { get; set; } = null!;
    }
}
