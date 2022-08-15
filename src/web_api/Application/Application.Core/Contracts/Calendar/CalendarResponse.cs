namespace Application.Core.Contracts
{
    public class CalendarResponse
    {
        public Guid id { get; set; }
        public DateTime? calendar_date { get; set; }
        public string company_cd { get; set; }
        public int? open_status { get; set; }
    }
}
