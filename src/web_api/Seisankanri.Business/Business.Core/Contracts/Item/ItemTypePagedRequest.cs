using Framework.Core.Collections;

namespace Business.Core.Contracts
{
    public class ItemTypePagedRequest : PagedRequest
    { 
        public string? type { get; set; }

        public string? code { get; set; }

        public string? name { get; set; }
    }
}
