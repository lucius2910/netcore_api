using Framework.Core.Abstractions;

namespace Application.Core.Contracts
{
    public class UserSearchRequest : IRequestPaged
    {
        public int page { get; set; } = 1;

        public int size { get; set; } = 10;

        public string? sort { get; set; }

        public string? role_cd { get; set; }
        public string? code { get; set; }

        public bool is_actived { get; set; }

        public string? branch_cd { get; set; }

        public string? deparment_cd { get; set; }

        public string? jobtitle_cd { get; set; }

        public string? full_name { get; set; }

    }
}

