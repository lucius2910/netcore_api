using Framework.Core.Entities;

namespace Seisankanri.Model.Entities
{
    public partial class Role : BaseEntity
    {
        public string code { get; set; }

        public string name { get; set; }

        public string? description { get; set; }

        public string is_actived { get; set; }

        public virtual ICollection<User>? users { get; set; }

        public virtual ICollection<Permission> permissions { get; set; }

        public Role()
        {
            id = Guid.NewGuid();
            this.permissions = new HashSet<Permission>();
        }
    }
}
