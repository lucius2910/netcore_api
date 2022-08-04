using Framework.Core.Entities;

namespace Seisankanri.Model.Entities
{
    public partial class ProductPlanMonth : BaseEntity
    {
        public DateTime plan_month { get; set; }

        public string item_cd { get; set; }

        public string item_type { get; set; }

        public string item_name1 { get; set; }

        public string item_name2 { get; set; }

        public string? machine_names { get; set; }

        public int? pre_month_inventory { get; set; }

        public int? to_be_sold { get; set; }

        public int? production_schedule { get; set; }

        public int? production_results { get; set; }

        public int? shipment_results { get; set; }

        public int? estimate_inventory { get; set; }

        public Item? item { get; set; }
    }
}
