using Domain.Abstractions;

namespace Domain.Entities
{
    public class AcceptanceInfo : BaseEntity
    {
        public int? po_seq { get; set; }
        public int rg_seq { get; set; }
        public string item_cd { get; set; }
        public string item_type { get; set; }
        public string item_name1 { get; set; }
        public string item_name2 { get; set; }
        public string unit { get; set; }
        public string unit2 { get; set; }
        public int rg_qty { get; set; }
        public int rg_qty2 { get; set; }
        public DateTime rg_date { get; set; }
        public int? inferior_qty { get; set; }
        public string remarks1 { get; set; }
        public string remarks2 { get; set; }
        public string m_item_cd { get; set; }
        public string m_item_type { get; set; }
        public string m_item_name1 { get; set; }
        public string m_item_name2 { get; set; }
        public string m_unit { get; set; }
        public int m_rg_qty { get; set; }
        public string r_order_cd { get; set; }
        public Item item { get; set; }

        public AcceptanceInfo()
        {
            id = Guid.NewGuid();
        }
    }
}
