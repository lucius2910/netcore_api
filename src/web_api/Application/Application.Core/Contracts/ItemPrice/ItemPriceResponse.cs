namespace Application.Core.Contracts
{
    public class ItemPriceResponse
    {
        public Guid item_id { get; set; }
        public string item_type { get; set; }
        public string item_code { get; set; }
        public string item_name1 { get; set; }
        public string item_name2 { get; set; }

        public Guid id { get; set; }
        public string item_cd { get; set; }
        public int? buy_price { get; set; }
        public int? sale_price { get; set; }
        public string? unit { get; set; }
        public int? basic_qty { get; set; }
        public int? meterial_price { get; set; }
        public int? process_price { get; set; }
        public int? auxiliary_price { get; set; }
        public DateTime? effective_startdate { get; set; }
        public DateTime? effective_enddate { get; set; }
        public string? currency { get; set; }
        public Guid? customer { get; set; }
        public int? min_qty { get; set; }
    }
}
