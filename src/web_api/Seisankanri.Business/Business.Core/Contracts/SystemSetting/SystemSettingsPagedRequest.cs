using Framework.Core.Collections;

namespace Business.Core.Contracts
{
    public class SystemSettingsPagedRequest : PagedRequest
    {
        public string? search { get; set; }
    }
}
