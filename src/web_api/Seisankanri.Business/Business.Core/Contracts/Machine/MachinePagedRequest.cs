using Framework.Core.Collections;

namespace Business.Core.Contracts
{
    public class MachinePagedRequest : PagedRequest
    {
        public string? search { get; set; }
    }
}
