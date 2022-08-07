using Framework.Core.Entities;

namespace Seisankanri.Model.Entities
{
    public partial class User : BaseEntity
    {
        public string employee_cd { get; set; }

        public string user_name { get; set; }

        public string hash_pass { get; set; }

        public string salt { get; set; }

        public bool is_actived { get; set; }

        public string? token { get; set; }

        public virtual ICollection<Role>? roles { get; set; }

        public User()
        {
            id = Guid.NewGuid();
        }
    }
}
