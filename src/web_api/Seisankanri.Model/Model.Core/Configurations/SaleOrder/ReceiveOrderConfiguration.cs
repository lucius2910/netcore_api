using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seisankanri.Model.Entities;

namespace Seisankanri.Model.Configurations
{
    public class ReceiveOrderConfiguration : IEntityTypeConfiguration<ReceiveOrder>
    {
        public void Configure(EntityTypeBuilder<ReceiveOrder> builder)
        {
            builder.ToTable("receive_order", "public");

            builder.Property(t => t.code).HasMaxLength(15).IsRequired();
            builder.Property(t => t.order_date).IsRequired();
            builder.Property(t => t.company_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.user_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.branch_cd).HasMaxLength(15);
            builder.Property(t => t.order_status).HasMaxLength(15);
            builder.Property(t => t.remarks).HasMaxLength(160);

            builder
            .HasOne(x => x.company)
            .WithMany(y => y.receive_order_companys)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.company_cd)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.branch)
            .WithMany(y => y.receive_order_branchs)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.branch_cd)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.user)
            .WithMany(y => y.receive_orders)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.user_cd)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
