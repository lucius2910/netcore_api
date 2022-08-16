using Domain.Abstractions;

namespace Domain.Entities
{
    public partial class User : BaseEntity
    {

        public string code { get; set; }

        public string full_name { get; set; }

        public string user_name { get; set; }

        public string? hashpass { get; set; }

        public string? salt { get; set; }

        public string? mail { get; set; }

        public string? phone { get; set; }

        public bool? is_actived { get; set; }

        public virtual ICollection<UserRole>? user_roles { get; set; }

        public virtual ICollection<UserToken>? user_token { get; set; }

        public User()
        {
            id = Guid.NewGuid();
        }
    }
}
