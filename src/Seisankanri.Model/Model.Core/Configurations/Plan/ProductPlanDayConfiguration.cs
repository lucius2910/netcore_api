using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seisankanri.Model.Entities;

namespace Seisankanri.Model.Configurations
{
    public class ProductPlanDayConfiguration : IEntityTypeConfiguration<ProductPlanDay>
    {
        public void Configure(EntityTypeBuilder<ProductPlanDay> builder)
        {
            builder.ToTable("product_plan_day", "public");

            builder.Property(t => t.plan_day).IsRequired();
            builder.Property(t => t.item_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.item_type).HasMaxLength(15).IsRequired();
            builder.Property(t => t.item_name1).HasMaxLength(160).IsRequired();
            builder.Property(t => t.item_name2).HasMaxLength(160).IsRequired();
            builder.Property(t => t.machine_cd).HasMaxLength(15);

            builder
            .HasOne(x => x.item)
            .WithMany(x => x.product_plan_days)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.item_cd)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.machine)
            .WithMany(x => x.product_plan_days)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.machine_cd)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}



