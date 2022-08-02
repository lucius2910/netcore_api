using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seisankanri.Model.Entities;

namespace Seisankanri.Model.Configurations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("inventory", "public");

            builder.Property(t => t.code).HasMaxLength(50);
            builder.Property(t => t.name).HasMaxLength(250);
            builder.Property(t => t.address).HasMaxLength(200);
        }
    }
}
