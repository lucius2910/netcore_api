using Framework.Core.Entities;

namespace Seisankanri.Model.Entities
{
    public class ReceiveOrder : BaseEntity
    {
        public string code { get; set; }

        public DateTime order_date { get; set; }

        public string company_cd { get; set; }

        public string user_cd { get; set; }

        public decimal? s_total_cost { get; set; }

        public string? branch_cd { get; set; }

        public string? order_status { get; set; }

        public string? remarks { get; set; }

        public virtual User? user { get; set; }

        public virtual Company? company { get; set; }

        public virtual Company? branch { get; set; }

        public virtual ICollection<ReceiveOrderDt>? details { get; set; }

        public ReceiveOrder()
        {
            id = Guid.NewGuid();
        }
    }
}
