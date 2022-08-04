namespace Business.SaleOrder.Contracts
{
    public class ReceiveOrderResponse
    {
        public string? code { get; set; }
        public string? order_date { get; set; }
        public string? company_cd { get; set; }
        public string? company_name { get; set; }
        public string? customer_person_name { get; set; }
        public string? company_tel_no { get; set; }
        public string? company_fax_no { get; set; }
        public string? order_status { get; set; }
        public string? remarks { get; set; }
        public Guid id { get; set; }


    }
}
