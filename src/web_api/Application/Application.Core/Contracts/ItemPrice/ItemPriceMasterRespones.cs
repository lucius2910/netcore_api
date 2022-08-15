namespace Application.Core.Contracts
{
    public class ItemPriceMasterRespones
    {
        public string item_type { get; set; }
        public string code { get; set; }
        public string name1 { get; set; }
        public string name2 { get; set; }
        public List<ItemBuyPriceRespones> buy_prices { get; set; }
        public List<ItemSalePriceRespones> sale_prices { get; set; }
    }
}
