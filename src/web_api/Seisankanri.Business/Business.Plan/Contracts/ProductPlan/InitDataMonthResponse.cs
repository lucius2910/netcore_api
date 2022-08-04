

using Framework.Core.Extensions;

namespace Business.Plan.Contracts
{
    public class InitDataMonthResponse
    {
        public string plan_month { get; set; }
        public string item_cd { get; set; }
        public string item_type { get; set; }
        public string item_name1 { get; set; }
        public string item_name2 { get; set; }
        public string? machine { get; set; }
        public int? pre_month_inventory { get; set; }
        public int? to_be_sold { get; set; }
        public int? production_schedule { get; set; }
        public int? production_results { get; set; }
        public int? shipment_results { get; set; }
        public int? estimate_inventory { get; set; }
        public DateTime? plan_month_fomated
        {
            get { return plan_month.ToDate() != null ? plan_month.ToDate() : null; }
        }
    }
}
