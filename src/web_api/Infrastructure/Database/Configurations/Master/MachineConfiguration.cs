using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Domain.Configurations
{
    public class MachineConfiguration : IEntityTypeConfiguration<Machine>
    {
        public void Configure(EntityTypeBuilder<Machine> builder)
        {
            builder.ToTable("machine");

            builder.Property(t => t.code).HasMaxLength(15).IsRequired();
            builder.Property(t => t.name).HasMaxLength(15).IsRequired();
            builder.Property(t => t.department_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.supplier_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.admin_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.type).HasMaxLength(15);
            builder.Property(t => t.mold_cd).HasMaxLength(15);
            builder.Property(t => t.color).HasMaxLength(15);
            builder.Property(t => t.effective_date).IsRequired();
            builder.Property(t => t.remarks).HasMaxLength(240);

            builder
            .HasOne(x => x.department)
            .WithMany(y => y.machines)
            .HasPrincipalKey(w => w.code2)
            .HasForeignKey(z => z.department_cd)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.classification_type)
            .WithMany(y => y.machines_type)
            .HasPrincipalKey(w => w.code2)
            .HasForeignKey(z => z.type)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.supplier)
            .WithMany(y => y.machines)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.supplier_cd)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.admin)
            .WithMany(y => y.machines)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.admin_cd)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
