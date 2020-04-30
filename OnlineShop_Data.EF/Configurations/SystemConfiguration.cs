using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop_Data.EF.Extensions;
using OnlineShop_Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop_Data.EF.Configurations
{
    public class SystemConfiguration : DbEntityConfiguration<SystemConfig>
    {
        public override void Configure(EntityTypeBuilder<SystemConfig> entity)
        {
            entity.Property(c => c.Id).HasMaxLength(255).IsRequired();
            // etc.
        }
    }
}
