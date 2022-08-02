

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seisankanri.Model.Entities;

namespace Model.Core.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("category","public");

            builder.Property(t => t.code).HasMaxLength(50).IsRequired();
            builder.Property(t => t.name).HasMaxLength(100).IsRequired();
        }
    }
}
