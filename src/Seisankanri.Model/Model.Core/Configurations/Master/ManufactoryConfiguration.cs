﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seisankanri.Model.Entities;

namespace Seisankanri.Model.Configurations
{
    public class ManufactoryConfiguration : IEntityTypeConfiguration<Manufactory>
    {
        public void Configure(EntityTypeBuilder<Manufactory> builder)
        {
            builder.ToTable("manufactory", "public");

            builder.Property(t => t.code).HasMaxLength(50);
            builder.Property(t => t.name).HasMaxLength(250);
            builder.Property(t => t.address).HasMaxLength(200);
        }
    }
}