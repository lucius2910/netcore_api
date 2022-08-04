using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seisankanri.Model.Entities;

namespace Seisankanri.Model.Configurations
{
    public class LogActionConfiguration : IEntityTypeConfiguration<LogAction>
    {
        public void Configure(EntityTypeBuilder<LogAction> builder)
        {
            builder.ToTable("log_action", "public");

            builder.Property(t => t.path).HasMaxLength(250);
            builder.Property(t => t.method).HasMaxLength(10);
            builder.Property(t => t.ip).HasMaxLength(30);
        }
    }
}
