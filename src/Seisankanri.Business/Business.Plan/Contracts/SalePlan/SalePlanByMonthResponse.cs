namespace Business.Plan.Contracts
{
    public class SalePlanByMonthResponse
    {
        public string? company_cd { get; set; }
        public string? item_type { get; set; }
        public string? item_cd { get; set; }
        public string? item_edi_cd { get; set; }
        public string? item_name1 { get; set; }
        public string? item_name2 { get; set; }
        public string? item_class_name2 { get; set; }
        public string? order_unit { get; set; }
        public int? standad_unit_qty { get; set; }
        public List<SalePlanByMonthItemResponse> data { get; set; }
        public double total
        {
            get
            {
                if(this.data != null && this.data.Count > 0)
                    return this.data.Sum(x => x.order_qty.HasValue ? (double)x.order_qty.Value : 0);
                else
                    return 0;
            }
        }
    }
}
