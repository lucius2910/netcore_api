using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Domain.Configurations
{
    public class ReceiveOrderConfiguration : IEntityTypeConfiguration<ReceiveOrder>
    {
        public void Configure(EntityTypeBuilder<ReceiveOrder> builder)
        {
            builder.ToTable("receive_order", "public");

            builder.Property(t => t.order_date).IsRequired();
            builder.Property(t => t.code).HasMaxLength(8).IsRequired();
            //builder.Property(t => t.kakuteru_no).HasMaxLength(8);
            //builder.Property(t => t.customer_cd).HasMaxLength(15).IsRequired();
            //builder.Property(t => t.destination_cd).HasMaxLength(15).IsRequired();
            //builder.Property(t => t.item_k).HasMaxLength(1).IsRequired();
            //builder.Property(t => t.item_cd).HasMaxLength(15).IsRequired();
            //builder.Property(t => t.order_qty).HasMaxLength(30).IsRequired();
            //builder.Property(t => t.case_qty).HasMaxLength(30);
            //builder.Property(t => t.case_m).HasMaxLength(30);
            //builder.Property(t => t.order_status_k).HasMaxLength(1).IsRequired();
            //builder.Property(t => t.order_ship_dt).IsRequired();
            //builder.Property(t => t.order_delivery_dt).IsRequired();
            //builder.Property(t => t.product_remain_qty).HasMaxLength(30);
            //builder.Property(t => t.destination_remain_qty).HasMaxLength(30);
            //builder.Property(t => t.remarks).HasMaxLength(160);

            //builder
            //.HasOne(x => x.company_customer_cd)
            //.WithMany(y => y.receive_order_cutomers)
            //.HasPrincipalKey(w => w.code)
            //.HasForeignKey(z => z.customer_cd)
            //.OnDelete(DeleteBehavior.Restrict);

            //builder
            //.HasOne(x => x.company_destination_cd)
            //.WithMany(y => y.receive_order_destinations)
            //.HasPrincipalKey(w => w.code)
            //.HasForeignKey(z => z.destination_cd)
            //.OnDelete(DeleteBehavior.Restrict);

            //builder
            //.HasOne(x => x.item)
            //.WithMany(y => y.item_receive_orders)
            //.HasPrincipalKey(w => w.code)
            //.HasForeignKey(z => z.item_cd)
            //.IsRequired(false)
            //.OnDelete(DeleteBehavior.Restrict);
        }
    }
}
