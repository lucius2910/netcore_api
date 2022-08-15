using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Domain.Configurations
{
    public class ClassificationConfiguration : IEntityTypeConfiguration<Classification>
    {
        public void Configure(EntityTypeBuilder<Classification> builder)
        {
            builder.ToTable("classifications", "public");

            builder.Property(t => t.code1).HasMaxLength(15).IsRequired();
            builder.Property(t => t.code2).HasMaxLength(15).IsRequired();
            builder.Property(t => t.name1).HasMaxLength(160).IsRequired();
            builder.Property(t => t.name2).HasMaxLength(160);
            builder.Property(t => t.value1).HasMaxLength(160);
            builder.Property(t => t.value2).HasMaxLength(160);
            builder.Property(t => t.remarks).HasMaxLength(240);
        }
    }
}
