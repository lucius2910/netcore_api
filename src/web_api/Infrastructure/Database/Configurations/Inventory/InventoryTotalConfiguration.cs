using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Domain.Configurations
{
    public class InventoryTotalConfiguration : IEntityTypeConfiguration<InventoryTotal>
    {
        public void Configure(EntityTypeBuilder<InventoryTotal> builder)
        {
            builder.ToTable("inventory_total", "public");

            builder.Property(t => t.branch_cd).HasMaxLength(15);
            builder.Property(t => t.inventory_k).HasMaxLength(2);
            builder.Property(t => t.inventory_cd).HasMaxLength(15);
            builder.Property(t => t.item_cd).HasMaxLength(15);
            builder.Property(t => t.item_div).HasMaxLength(2);
            builder.Property(t => t.inspection_k).HasMaxLength(2);
            builder.Property(t => t.location).HasMaxLength(15);
            builder.Property(t => t.lot_cd).HasMaxLength(15);
            builder.Property(t => t.silo_k).HasMaxLength(2);
            builder.Property(t => t.remark).HasMaxLength(240);
            builder.Property(t => t.last_whin_date).HasMaxLength(10);
            builder.Property(t => t.last_whout_date).HasMaxLength(10);
            builder.Property(t => t.last_invt_date).HasMaxLength(10);
            builder.Property(t => t.stock_cutoff_date).HasMaxLength(10);

            builder
            .HasOne(x => x.item)
            .WithMany(x => x.inventory_totals)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.item_cd)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
