
using Framework.Core.Extensions;

namespace Business.Plan.Contracts
{
    public class InitDataByDayResponse
    {
        public string plan_day { get; set; }
        public string item_cd { get; set; }
        public string item_type { get; set; }
        public string item_name1 { get; set; }
        public string item_name2 { get; set; }
        public int? to_be_sold { get; set; }
        public string? machine_cd { get; set; }
        public int? production_schedule { get; set; }
        public int? production_results { get; set; }
        public int? shipment_results { get; set; }
        public int? estimate_shipment_results { get; set; }
        public int? estimate_inventory { get; set; }
        public DateTime? plan_day_fomated
        {
            get { return plan_day.ToDate() != null ? plan_day.ToDate() : null; }
        }
    }
}
