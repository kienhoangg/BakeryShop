﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop_Data.EF.Extensions;
using OnlineShop_Data.Entities;

namespace OnlineShop_Data.EF.Configurations
{
    public class ContactDetailConfiguration : DbEntityConfiguration<Contact>
    {
        public override void Configure(EntityTypeBuilder<Contact> entity)
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).HasMaxLength(255).IsRequired();
            // etc.
        }
    }
}
