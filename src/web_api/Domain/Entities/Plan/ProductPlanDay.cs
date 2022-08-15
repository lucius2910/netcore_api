using Domain.Abstractions;

namespace Domain.Entities
{
    public partial class ProductPlanDay : BaseEntity
    {
        public DateTime plan_day { get; set; }

        public string item_cd { get; set; }

        public string item_type { get; set; }

        public string item_name1 { get; set; }

        public string item_name2 { get; set; }

        public int? to_be_sold { get; set; }

        public string? machine_cd { get; set; }

        public int? production_schedule { get; set; }

        public int? production_schedule_2 { get; set; }

        public int? production_results { get; set; }

        public int? production_results_2 { get; set; }

        public int? shipment_results { get; set; }

        public int? shipment_results_2 { get; set; }

        public int? estimate_shipment_results { get; set; }

        public Item item { get; set; }

        public Machine? machine { get; set; }

        public ProductPlanDay()
        {

        }
    }
}
