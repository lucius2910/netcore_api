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
            builder.Property(t => t.deparment_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.branch_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.jobtitle_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.furigana).HasMaxLength(100).IsRequired();


            builder
            .HasOne(x => x.role)
            .WithMany(y => y.users)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.role_cd)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.branch)
            .WithMany(y => y.users)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.branch_cd)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.department)
            .WithMany(y => y.user_departments)
            .HasPrincipalKey(w => w.code2)
            .HasForeignKey(z => z.deparment_cd)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.jobtitle)
            .WithMany(y => y.user_jobtitles)
            .HasPrincipalKey(w => w.code2)
            .HasForeignKey(z => z.jobtitle_cd)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
