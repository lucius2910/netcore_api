
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Model.Core.Configurations
{
    public class FunctionConfiguration : IEntityTypeConfiguration<Function>
    {
        public void Configure(EntityTypeBuilder<Function> builder)
        {
            builder.ToTable("function", "public");

            builder.Property(t => t.module).HasMaxLength(50);
            builder.Property(t => t.code).HasMaxLength(50).IsRequired();
            builder.Property(t => t.name).HasMaxLength(250);
            builder.Property(t => t.description).HasMaxLength(500);
            builder.Property(t => t.url).HasMaxLength(250);
            builder.Property(t => t.path).HasMaxLength(250);
            builder.Property(t => t.method).HasMaxLength(10);
        }
    }
}
