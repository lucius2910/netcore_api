using Framework.Core.Collections;

namespace Business.Core.Contracts
{
    public class CategoryPagedRequest : PagedRequest
    {
        public string? search { get; set; }
    }
}
