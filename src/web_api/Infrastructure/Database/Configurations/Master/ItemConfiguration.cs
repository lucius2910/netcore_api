using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Domain.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("item", "public");

            builder.Property(t => t.item_type).HasMaxLength(10);
            builder.Property(t => t.code).HasMaxLength(15).IsRequired();
            builder.Property(t => t.short_name_kana).HasMaxLength(32).IsRequired();
            builder.Property(t => t.name1).HasMaxLength(160).IsRequired();
            builder.Property(t => t.name2).HasMaxLength(160).IsRequired();
            builder.Property(t => t.model_number).HasMaxLength(160);
            builder.Property(t => t.account_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.std_unit).HasMaxLength(15).IsRequired();
            builder.Property(t => t.std_unitprice).IsRequired();
            builder.Property(t => t.tax_div).HasMaxLength(15).IsRequired();
            builder.Property(t => t.procure_div).HasMaxLength(15);
            builder.Property(t => t.provide_div).HasMaxLength(15);
            builder.Property(t => t.prod_lt).IsRequired();
            builder.Property(t => t.drawing_no).HasMaxLength(40);
            builder.Property(t => t.standard).HasMaxLength(40);
            builder.Property(t => t.place_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.account_cd2).HasMaxLength(6);
            builder.Property(t => t.class_cd1).HasMaxLength(15);
            builder.Property(t => t.class_cd2).HasMaxLength(15);
            builder.Property(t => t.class_cd3).HasMaxLength(15);
            builder.Property(t => t.unit2).HasMaxLength(16).IsRequired();
            builder.Property(t => t.unit_conv_rate).IsRequired();
            builder.Property(t => t.stock_admin_div).HasMaxLength(15);
            builder.Property(t => t.admin_div).HasMaxLength(15);
            builder.Property(t => t.per_weight).IsRequired();
            builder.Property(t => t.calc_div).HasMaxLength(15);
            builder.Property(t => t.unit_conv_f).HasMaxLength(1);
            builder.Property(t => t.machine_cd).HasMaxLength(15);
            builder.Property(t => t.inspect_div).HasMaxLength(8);
            builder.Property(t => t.lot_admin_div).HasMaxLength(15);
            builder.Property(t => t.po_slip_issue_f).HasMaxLength(1);
            builder.Property(t => t.company_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.item_name3).HasMaxLength(160);
            builder.Property(t => t.item_name4).HasMaxLength(160);
            builder.Property(t => t.care_div).HasMaxLength(15);
            builder.Property(t => t.class_cd4).HasMaxLength(15);
            builder.Property(t => t.class_cd5).HasMaxLength(15);
            builder.Property(t => t.class_cd6).HasMaxLength(15);
            builder.Property(t => t.material_quality_cd).HasMaxLength(15);
            builder.Property(t => t.calc_by_prod_f).HasMaxLength(1);
            builder.Property(t => t.prod_arrange_div).HasMaxLength(15);
            builder.Property(t => t.location).HasMaxLength(30);
            builder.Property(t => t.invalid_f).HasMaxLength(1);

            builder
            .HasOne(x => x.type)
            .WithMany(y => y.item_types)
            .HasPrincipalKey(w => w.code2)
            .HasForeignKey(z => z.item_type)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.machine)
            .WithMany(y => y.items)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.machine_cd)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.place)
            .WithMany(y => y.item_places)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.place_cd)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.company)
            .WithMany(y => y.item_companys)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.company_cd)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}