namespace Business.Core.Contracts
{
    public class SystemSettingsResponse
    {
        public Guid id { get; set; }
        public string? code { get; set; }
        public string? name { get; set; }
        public string? value { get; set; }
    }
}
