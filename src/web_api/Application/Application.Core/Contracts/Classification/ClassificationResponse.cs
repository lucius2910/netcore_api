namespace Application.Core.Contracts
{
    public class ClassificationResponse
    {
        public Guid id { get; set; }
        public string code1 { get; set; }
        public string code2 { get; set; }
        public string name1 { get; set; }
        public string? name2 { get; set; }
        public string? value1 { get; set; }
        public string? value2 { get; set; }
        public string? remarks { get; set; }
    }
}
