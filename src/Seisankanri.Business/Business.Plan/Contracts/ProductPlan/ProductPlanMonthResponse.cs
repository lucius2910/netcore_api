namespace Business.Plan.Contracts
{
    public class ProductPlanByMonthResponse
    {
        public string? item_cd { get; set; }
        public string? item_name1 { get; set; }
        public string? item_class_name2 { get; set; }
        public string? machine_name { get; set; }
        public int? pre_month_inventory { get; set; }
        public int? to_be_sold { get; set; }
        public int? production_schedule { get; set; }
        public int? production_results { get; set; }
        public int? shipment_results { get; set; }
        public int? estimate_inventory { get; set; }
        public string? item_edi_cd { get; set; }
    }
}
