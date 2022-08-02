using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seisankanri.Model.Entities;

namespace Seisankanri.Model.Configurations
{
    public class ItemPriceConfiguration : IEntityTypeConfiguration<ItemPrice>
    {
        public void Configure(EntityTypeBuilder<ItemPrice> builder)
        {
            builder.ToTable("item_price", "public");

            builder.Property(t => t.item_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.unit).HasMaxLength(15);
            builder.Property(t => t.currency).HasMaxLength(3);


            builder
             .HasOne(x => x.item)
             .WithMany(y => y.item_prices)
             .HasPrincipalKey(w => w.code)
             .HasForeignKey(z => z.item_cd)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
