using Framework.Core.Collections;

namespace Business.Inventories.Contracts
{
    public class InventoryPagedRequest : PagedRequest
    {
        public string? search { get; set; }
    }
}
