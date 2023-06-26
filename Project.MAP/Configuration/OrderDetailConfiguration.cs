using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Configuration
{
    public class OrderDetailConfiguration : BaseConfiguration<OrderDetail>
    {
        public override void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.SubTotal).HasColumnType("decimal(18,2)");
            builder.Ignore(x => x.Id);
            builder.HasKey(x => new
            {
                x.ProductId,
                x.OrderId
            });
        }
    }
}
