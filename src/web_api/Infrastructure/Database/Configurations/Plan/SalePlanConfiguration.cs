using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Configurations
{
    public class SalePlanConfiguration : IEntityTypeConfiguration<SalePlan>
    {
        public void Configure(EntityTypeBuilder<SalePlan> builder)
        {
            builder.ToTable("sale_plan", "public");

            builder.Property(t => t.company_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.item_type).HasMaxLength(15).IsRequired();
            builder.Property(t => t.item_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.item_edi_cd).HasMaxLength(15);
            builder.Property(t => t.item_name1).HasMaxLength(160).IsRequired();
            builder.Property(t => t.item_name2).HasMaxLength(160);
            builder.Property(t => t.order_unit).HasMaxLength(15).IsRequired();
            builder.Property(t => t.standad_unit_qty).IsRequired();
            builder.Property(t => t.order_ym).HasColumnType(DataType.Date.ToString()).IsRequired();

            builder
            .HasOne(x => x.company)
            .WithMany(x => x.sale_plan_companys)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.company_cd)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.item)
            .WithMany(x => x.item_sale_plans)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.item_cd)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.item_edi)
            .WithMany(x => x.sale_plans)
            .HasPrincipalKey(w => w.edi_cd)
            .HasForeignKey(z => z.item_edi_cd)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
