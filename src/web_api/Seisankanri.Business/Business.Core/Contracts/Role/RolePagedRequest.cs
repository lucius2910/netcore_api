using Framework.Core.Collections;

namespace Business.Core.Contracts
{
    public class RolePagedRequest : PagedRequest
    {
        public string? search { get; set; }
    }
}
