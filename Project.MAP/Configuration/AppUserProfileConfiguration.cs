using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Configuration
{
    public class AppUserProfileConfiguration : BaseConfiguration<AppUserProfile>
    {
        public override void Configure(EntityTypeBuilder<AppUserProfile> builder)
        {
            base.Configure(builder);

            builder.Ignore(x => x.FullName);

            AppUserProfile adminProfile = new()
            {
                Id = "5c8defd5-91f2-4256-9f16-e7fa7546dec4",
                FirstName = "Tuğberk",
                LastName = "Mehdioğlu"
            };

            builder.HasData(adminProfile);
        }
    }
}
