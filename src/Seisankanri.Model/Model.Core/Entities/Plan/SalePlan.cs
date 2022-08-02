using Framework.Core.Entities;

namespace Seisankanri.Model.Entities
{
    public class SalePlan : BaseEntity
    {
        public string company_cd { get; set; }

        public string item_type { get; set; }

        public string item_cd { get; set; }

        public string? item_edi_cd{ get; set; }

        public string item_name1 { get; set; }

        public string? item_name2 { get; set; }

        public string order_unit { get; set; }

        public int standad_unit_qty { get; set; }

        public DateTime order_ym { get; set; }

        public int? order_qty { get; set; }

        public Company company { get; set; }

        public Item item { get; set; }

        public ItemEdi item_edi { get; set; }

        public SalePlan()
        {
            id = Guid.NewGuid();
        }
    }
}
