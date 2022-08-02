using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seisankanri.Model.Entities;

namespace Seisankanri.Model.Configurations
{
    public class ReceiveOrderDtConfiguration : IEntityTypeConfiguration<ReceiveOrderDt>
    {
        public void Configure(EntityTypeBuilder<ReceiveOrderDt> builder)
        {
            builder.ToTable("receive_order_dt", "public");

            builder.Property(t => t.r_order_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.company_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.item_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.item_name).HasMaxLength(160);
            builder.Property(t => t.warehouse_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.delivery_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.remarks).HasMaxLength(160);

            builder
            .HasOne(x => x.receive_order)
            .WithMany(y => y.details)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.r_order_cd)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.item)
            .WithMany(y => y.receive_order_dts)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.item_cd)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.warehouse)
            .WithMany(y => y.receive_order_dt_warehouses)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.warehouse_cd)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.delivery)
            .WithMany(y => y.receive_order_dt_deliverys)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.delivery_cd)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}