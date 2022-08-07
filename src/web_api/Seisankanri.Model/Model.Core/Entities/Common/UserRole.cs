using Framework.Core.Entities;

namespace Seisankanri.Model.Entities
{
    public partial class UserRole : BaseEntity
    {

        public string employee_cd { get; set; }

        public string role_cd { get; set; }

        public virtual Employee? employee { get; set; }

        public virtual Role? role { get; set; }

        public UserRole()
        {
            id = Guid.NewGuid();
        }
    }
}
