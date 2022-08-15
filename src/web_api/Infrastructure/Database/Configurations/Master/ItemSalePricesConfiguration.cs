using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Domain.Configurations
{
    public class ItemSalePricesConfiguration : IEntityTypeConfiguration<ItemSalePrices>
    {
        public void Configure(EntityTypeBuilder<ItemSalePrices> builder)
        {
            builder.ToTable("item_sale_prices", "public");

            builder.Property(t => t.code).HasMaxLength(15).IsRequired();
            builder.Property(t => t.item_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.unit).HasMaxLength(15).IsRequired();
            builder.Property(t => t.price).IsRequired();
            builder.Property(t => t.effective_startdate).IsRequired();
            builder.Property(t => t.currency).HasMaxLength(3);
            builder.Property(t => t.min_qty).IsRequired();

            builder
            .HasOne(x => x.item)
            .WithMany(y => y.item_sale_prices)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.item_cd)
            .OnDelete(DeleteBehavior.Restrict);
        }

    }
}