using Domain.Abstractions;

namespace Domain.Entities
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

        public Item? item { get; set; }
        public string? unit { get; set; }
        public int? pre_month_inventory_2 { get; set; }
        public int? to_be_sold_2 { get; set; }
        public int? production_schedule_2 { get; set; }
        public int? production_results_2 { get; set; }
        public int? shipment_results_2 { get; set; }
        public string? unit_2 { get; set; }
        public int? required_results { get; set; }

    }
}
