using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Domain.Configurations{
    public class ItemMaterialsConfiguration : IEntityTypeConfiguration<ItemMaterials>
    {
        public void Configure(EntityTypeBuilder<ItemMaterials> builder)
        {
            builder.ToTable("item_materials", "public");

            builder.Property(t => t.id).IsRequired();
            builder.Property(t => t.item_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.m_type).HasMaxLength(15).IsRequired();
            builder.Property(t => t.m_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.m_name1).HasMaxLength(160).IsRequired();
            builder.Property(t => t.m_name2).HasMaxLength(160).IsRequired();
            builder.Property(t => t.unit).HasMaxLength(15).IsRequired();
            builder.Property(t => t.del_flg).IsRequired();
        }
    }
}