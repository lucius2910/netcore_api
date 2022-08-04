using Framework.Core.Collections;

namespace Business.Core.Contracts
{
    public class MasterCodePagedRequest : PagedRequest
    {
        public string? search { get; set; }
    }
}
