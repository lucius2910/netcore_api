namespace Application.Core.Contracts
{
    public class ItemBuyPriceRespones
    {
        public string code { get; set; }
        public string item_cd { get; set; }
        public int? price { get; set; }
        public string? unit { get; set; }
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
