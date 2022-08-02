using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seisankanri.Model.Entities;

namespace Seisankanri.Model.Configurations
{
    public class ItemEdiConfiguration : IEntityTypeConfiguration<ItemEdi>
    {
        public void Configure(EntityTypeBuilder<ItemEdi> builder)
        {
            builder.ToTable("item_edi", "public");

            builder.Property(t => t.company_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.item_type).HasMaxLength(15).IsRequired();
            builder.Property(t => t.item_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.item_name1).HasMaxLength(160).IsRequired();
            builder.Property(t => t.item_name2).HasMaxLength(160);
            builder.Property(t => t.edi_cd).HasMaxLength(15).IsRequired();

            builder.HasIndex(t => new { t.company_cd, t.item_cd, t.edi_cd }).IsUnique();

            builder
            .HasOne(x => x.company)
            .WithMany(x => x.item_edi_companys)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.company_cd)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.item)
            .WithMany(x => x.item_edis)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.item_cd)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
