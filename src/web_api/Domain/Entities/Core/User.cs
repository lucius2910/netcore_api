using Domain.Abstractions;

namespace Domain.Entities
{
    public partial class User : BaseEntity
    {
        public string? role_cd { get; set; }

        public string code { get; set; }

        public string full_name { get; set; }

        public string user_name { get; set; }

        public string? hashpass { get; set; }

        public string? salt { get; set; }

        public string? mail { get; set; }

        public string? phone { get; set; }

        public bool? is_actived { get; set; }

        public string deparment_cd { get; set; }

        public string branch_cd { get; set; }

        public string jobtitle_cd { get; set; }

        public string furigana { get; set; }

        public virtual Role? role { get; set; }

        public virtual Company? branch { get; set; }

        public virtual Classification? department { get; set; }

        public virtual Classification? jobtitle { get; set; }

        public ICollection<Machine>? machines { get; set; }

        public ICollection<UserToken>? usertoken { get; set; }

        public User()
        {
            id = Guid.NewGuid();
        }
    }
}
