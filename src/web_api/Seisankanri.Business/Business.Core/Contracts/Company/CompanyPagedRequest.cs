using Framework.Core.Collections;

namespace Business.Core.Contracts
{
    public class CompanyPagedRequest : PagedRequest
    {
        public string? search { get; set; }
    }
}
