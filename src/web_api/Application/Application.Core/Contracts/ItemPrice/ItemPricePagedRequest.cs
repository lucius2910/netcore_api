namespace Application.Core.Contracts
{
    public class ItemPricePagedRequest
    {
        public int page { get; set; } = 1;
        public int size { get; set; } = 10;
        public string? sort { get; set; }
        public string? item_type { get; set; }
        public string? item_code { get; set; }
        public string? item_name1 { get; set; }
        public string? item_name2 { get; set; }
    }
}
