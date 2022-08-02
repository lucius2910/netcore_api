﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seisankanri.Model.Entities;

namespace Seisankanri.Model.Configurations
{
    public class SeqConfiguration : IEntityTypeConfiguration<Seq>
    {
        public void Configure(EntityTypeBuilder<Seq> builder)
        {
            builder.ToTable("seq", "public");

            builder.Property(t => t.code).IsRequired();
            builder.Property(t => t.no).IsRequired();
        }
    }
}
