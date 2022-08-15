using Domain.Abstractions;

namespace Domain.Entities {
    public class ItemMaterials : BaseEntity
    {
        public string item_cd { get; set; }
        public string m_type { get; set; }
        public string m_cd { get; set; }
        public string m_name1 { get; set; }
        public string m_name2 { get; set; }
        public int ratio { get; set; }
        public int incidental_rate { get; set; }
        public string unit { get; set; }

        public ItemMaterials()
        {
            id = Guid.NewGuid();
        }
    }
}