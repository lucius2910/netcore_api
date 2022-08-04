using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seisankanri.Model.Entities;

namespace Seisankanri.Model.Configurations
{
    public class WarehousesConfiguration : IEntityTypeConfiguration<Warehouses>
    {
        public void Configure(EntityTypeBuilder<Warehouses> builder)
        {
            builder.ToTable("warehouses", "public");

            builder.Property(t => t.type).HasMaxLength(15).IsRequired();
            builder.Property(t => t.code).HasMaxLength(15).IsRequired();
            builder.Property(t => t.name).HasMaxLength(160).IsRequired();
            builder.Property(t => t.control_company).HasMaxLength(15).IsRequired();
            builder.Property(t => t.control_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.postcode).HasMaxLength(15).IsRequired();
            builder.Property(t => t.address).HasMaxLength(160);
            builder.Property(t => t.phone).HasMaxLength(15);
            builder.Property(t => t.capacity).HasMaxLength(15).IsRequired();
            builder.Property(t => t.rent_price).IsRequired();
            builder.Property(t => t.item_info).HasMaxLength(160);
            builder.Property(t => t.person_cd1).HasMaxLength(15);
            builder.Property(t => t.person_cd2).HasMaxLength(15);
            builder.Property(t => t.remarks).HasMaxLength(240);
        }
    }
}
