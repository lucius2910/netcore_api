using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seisankanri.Model.Entities;

namespace Seisankanri.Model.Configurations
{
    public class ItemBuyPricesConfiguration : IEntityTypeConfiguration<ItemBuyPrices>
    {
        public void Configure(EntityTypeBuilder<ItemBuyPrices> builder)
        {
            builder.ToTable("item_buy_prices", "public");

            builder.Property(t => t.code).HasMaxLength(15).IsRequired();
            builder.Property(t => t.item_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.unit).HasMaxLength(15);
            builder.Property(t => t.currency).HasMaxLength(3);

            builder
            .HasOne(x => x.item)
            .WithMany(y => y.item_buy_prices)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.item_cd)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}