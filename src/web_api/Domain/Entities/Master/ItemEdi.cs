using Domain.Abstractions;

namespace Domain.Entities
{
    public class ItemEdi : BaseEntity
    {
        public string company_cd { get; set; }

        public string item_type { get; set; }

        public string item_cd { get; set; }

        public string item_name1 { get; set; }

        public string? item_name2 { get; set; }

        public string edi_cd { get; set; }

        public DateTime export_date { get; set; }

        public Item item { get; set; }

        public Company company { get; set; }

        public ICollection<SalePlan> sale_plans { get; set; }

        public ItemEdi()
        {
            id = Guid.NewGuid();
        }
    }
}
