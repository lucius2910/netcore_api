using Framework.Core.Collections;

namespace Application.Sale.Contracts
{
    public class SalePlanSearchRequest : PagedRequest
    {
        public DateTime order_ym { get; set; }
        public string company_cd { get; set; }
        public string item_type { get; set; }

    }
}
