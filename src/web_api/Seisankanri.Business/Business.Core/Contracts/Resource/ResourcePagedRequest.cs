using Framework.Core.Collections;

namespace Business.Core.Contracts
{
    public class ResourcePagedRequest : PagedRequest
    {
        public string ? module { get; set; }

        public string ? screen { get; set; }
    }
}
