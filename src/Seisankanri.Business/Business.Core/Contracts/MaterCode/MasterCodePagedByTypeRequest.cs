using Framework.Core.Collections;

namespace Business.Core.Contracts
{
    public class MasterCodePagedByTypeRequest : PagedRequest
    {
        public string? type { get; set; }
    }
}
