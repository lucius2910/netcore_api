using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Domain.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user", "public");

            builder.Property(t => t.code).HasMaxLength(15).IsRequired();
            builder.Property(t => t.user_name).HasMaxLength(100).IsRequired();
            builder.Property(t => t.full_name).HasMaxLength(100).IsRequired();
            builder.Property(t => t.role_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.hashpass).HasMaxLength(100);
            builder.Property(t => t.salt).HasMaxLength(100);
            builder.Property(t => t.mail).HasMaxLength(100).IsRequired();
            builder.Property(t => t.phone).HasMaxLength(15);


            builder
            .HasOne(x => x.role)
            .WithMany(y => y.users)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.role_cd)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
