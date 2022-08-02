using Framework.Core.Entities;

namespace Seisankanri.Model.Entities
{
    public partial class SystemSettings : BaseEntity
    {
        public string code { get; set; }

        public string? name { get; set; }

        public string? value { get; set; }

    }
}
