using Framework.Core.Collections;

namespace Business.Core.Contracts
{
    public class ItemSearchRequest : PagedRequest
    {
        public string? item_type { get; set; }

        public string? code { get; set; }

        public string? name { get; set; }

        public string? name1 { get; set; }

        public string? name2 { get; set; }
    }
}
