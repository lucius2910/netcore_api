using Framework.Core.Entities;

namespace Seisankanri.Model.Entities
{
    public partial class MasterCode : BaseEntity
    {

        public string type { get; set; }

        public string key { get; set; }

        public string value { get; set; }

        public MasterCode()
        {
            id = Guid.NewGuid();
        }
    }
}