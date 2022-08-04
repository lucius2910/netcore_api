using Framework.Core.Collections;

namespace Business.Inventories.Contracts
{
    public class WarehousePagedRequest : PagedRequest
    {
        public string? search { get; set; }
    }
}
