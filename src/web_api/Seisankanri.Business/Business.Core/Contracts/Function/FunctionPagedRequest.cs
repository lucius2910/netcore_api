using Framework.Core.Collections;

namespace Business.Core.Contracts
{
    public class FunctionPagedRequest : PagedRequest
    {
        public string? module { get; set; }

        public string? parent { get; set; }

        public string? code { get; set; }

        public string? name { get; set; }
    }
}
