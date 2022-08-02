namespace Business.Plan.Contracts
{
    public class ProductPlanByDayResponse
    {
        public string item_cd { get; set; }
        public string item_name1 { get; set; }
        public int? estimate_sold { get; set; }
        public int? to_be_sold { get; set; }
        public int? pre_month_inventory { get; set; }
        public List<ProductPlanByDayItemResponse> data { get; set; }

    }
}

