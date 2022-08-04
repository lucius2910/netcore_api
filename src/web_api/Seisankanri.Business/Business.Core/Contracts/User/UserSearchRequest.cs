using Framework.Core.Collections;

namespace Business.Core.Contracts
{
    public class UserSearchRequest : PagedRequest
    {
        public string? role_cd { get; set; }
        public string code { get; set; }

        public bool? is_actived { get; set; }

        public string branch_cd { get; set; }

        public string deparment_cd { get; set; }

        public string jobtitle_cd { get; set; }

        public string full_name { get; set; }

    }
}

