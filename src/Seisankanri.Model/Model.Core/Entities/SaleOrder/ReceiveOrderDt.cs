using Framework.Core.Entities;

namespace Seisankanri.Model.Entities
{
    public class ReceiveOrderDt : BaseEntity
    {
        public string r_order_cd { get; set; }

        public string company_cd { get; set; }

        public string item_cd { get; set; }

        public string? item_name { get; set; }

        public string warehouse_cd { get; set; }

        public string delivery_cd { get; set; }

        public decimal? r_order_input { get; set; }

        public decimal? r_order_pieces_qty { get; set; }

        public decimal? r_order_qty { get; set; }

        public decimal? r_order_unit_price { get; set; }

        public decimal? r_order_cost { get; set; }

        public DateTime? r_order_dl { get; set; }

        public string? remarks { get; set; }

        public virtual ReceiveOrder receive_order { get; set; }

        public virtual Item? item { get; set; }

        public virtual Company? warehouse { get; set; }

        public virtual Company? delivery { get; set; }

        public ReceiveOrderDt()
        {
            id = Guid.NewGuid();
        }
    }
}
