using Seisankanri.Model.Core;

namespace Business.Core.Contracts
{
    public class UserResponse
    {
        public Guid id { get; set; }
        public string? role_cd { get; set; }
        public string? code { get; set; }
        public string? branch_cd { get; set; }
        public string? deparment_cd { get; set; }
        public string? jobtitle_cd { get; set; }
        public string? furigana { get; set; }
        public string? full_name { get; set; }
        public string? user_name { get; set; }
        public string? mail { get; set; }
        public string? phone { get; set; }
        public bool? is_active { get; set; }
        public List<string>? permissions { get; set; }
    }
}
