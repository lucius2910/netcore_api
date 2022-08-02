namespace Business.SaleOrder.Contracts
{
    public class ReceiveOrderDtResoponse
    {
        public Guid id { get; set; }
        public string? item_cd { get; set; }
        public string? item_name { get; set; }
        public string? delivery_cd { get; set; }
        public decimal r_order_input { get; set; }
        public decimal r_order_pieces_qty { get; set; }
        public decimal r_order_qty { get; set; }
        public decimal r_order_unit_price { get; set; }
        public decimal r_order_cost { get; set; }
        public string? r_order_dl { get; set; }
        public string? remarks { get; set; }
    }
}
