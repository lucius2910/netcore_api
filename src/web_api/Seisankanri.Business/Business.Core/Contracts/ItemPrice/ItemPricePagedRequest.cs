using Framework.Core.Collections;

namespace Business.Core.Contracts
{
    public class ItemPricePagedRequest : PagedRequest
    {
        public string? item_type { get; set; }
        public string? item_code { get; set; }
        public string? item_name1 { get; set; }
        public string? item_name2 { get; set; }
    }
}
