using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seisankanri.Model.Entities;

namespace Seisankanri.Model.Configurations
{
    public class CalendarConfiguration : IEntityTypeConfiguration<Calendar>
    {
        public void Configure(EntityTypeBuilder<Calendar> builder)
        {
            builder.ToTable("calendars", "public");

            builder.Property(t => t.company_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.open_status).IsRequired();

            builder
            .HasOne(x => x.company)
            .WithMany(x => x.calendars)
            .HasPrincipalKey(w => w.code)
            .HasForeignKey(z => z.company_cd)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
