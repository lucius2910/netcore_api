namespace Business.Plan.Contracts
{
    public class ProductPlanByDayItemResponse
    {
        public string? machine_cd { get; set; }
        public string? plan_day { get; set; }
        public int? to_be_sold { get; set; }
        public int? production_schedule { get; set; }
        public int? production_results { get; set; }
        public int? estimate_inventory { get; set; }
    }
}
