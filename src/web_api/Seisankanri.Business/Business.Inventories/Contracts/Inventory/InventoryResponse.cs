namespace Business.Inventories.Contracts
{
    public class InventoryResponse
    {
        public Guid Id { get; set; }
        public string? code { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public bool status { get; set; }
        public string? note { get; set; }
    }
}
