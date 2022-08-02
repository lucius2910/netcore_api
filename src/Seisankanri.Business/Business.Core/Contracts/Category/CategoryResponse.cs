namespace Business.Core.Contracts
{
    public class CategoryResponse
    {
        public Guid id { get; set; }
        public string? code { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
    }
}
