using Framework.Core.Entities;

namespace Seisankanri.Model.Entities
{
    public partial class Seq : BaseEntity
    {

        public string code { get; set; }

        public int no { get; set; }

        public Seq()
        {
            id = Guid.NewGuid();
        }

    }
}
