using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Configuration
{
    public class AddressConfiguration : BaseConfiguration<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);

            builder.Ignore(x => x.FullAddress);

            Address adminAddress = new()
            {
                Id = 1,
                Name = "Ev",
                Country = "Türkiye",
                City = "İstanbul",
                District = "Kağıthane",
                NeighborHood = "Çeliktepe",
                Street = "Şairane",
                AptNo = 21,
                Flat = 8,
                AppUserProfileId = "5c8defd5-91f2-4256-9f16-e7fa7546dec4"
            };

            builder.HasData(adminAddress);
        }
    }
}
