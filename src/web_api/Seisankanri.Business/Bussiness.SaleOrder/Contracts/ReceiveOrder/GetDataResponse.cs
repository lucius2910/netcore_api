namespace Business.SaleOrder.Contracts
{
    public class GetDataResponse
    {
        public string? order_company_name1 { get; set; }
        public int branch_f { get; set; }
        public string? order_post_code { get; set; }
        public string? order_address1 { get; set; }
        public string? order_tel_no { get; set; }
        public string? order_fax_no { get; set; }

        public string? delivery_company_name1 { get; set; }
        public string? customer_person_name { get; set; }
        public string? delivey_post_code { get; set; }
        public string? delivery_address1 { get; set; }
        public string? address2 { get; set; }
        public string? delivery_tel_no { get; set; }
        public string? delivery_fax_no { get; set; }

        public string? r_order_date { get; set; }

        public List<ReceiveOrderDtResoponse>? details { get; set; }

    }
}
