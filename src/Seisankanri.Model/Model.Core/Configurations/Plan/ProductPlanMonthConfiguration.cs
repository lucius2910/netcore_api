using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seisankanri.Model.Entities;

namespace Seisankanri.Model.Configurations
{
    public class ProductPlanMonthConfiguration : IEntityTypeConfiguration<ProductPlanMonth>
    {
        public void Configure(EntityTypeBuilder<ProductPlanMonth> builder)
        {
            builder.ToTable("product_plan_month", "public");

            builder.Property(t => t.plan_month).IsRequired();
            builder.Property(t => t.item_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.item_type).HasMaxLength(15).IsRequired();
            builder.Property(t => t.item_name1).HasMaxLength(160).IsRequired();
            builder.Property(t => t.item_name2).HasMaxLength(160).IsRequired();
            builder.Property(t => t.machine_names).HasMaxLength(250);

            builder
            .HasOne(x => x.item)
            .WithMany(x => x.product_plan_months)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.item_cd)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
