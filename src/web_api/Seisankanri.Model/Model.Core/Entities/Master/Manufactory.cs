using Framework.Core.Entities;

namespace Seisankanri.Model.Entities
{
    public class Manufactory : BaseEntity
    {
        public string? code { get; set; }

        public string? name { get; set; }

        public string? address { get; set; }

        public string? lat { get; set; }

        public string? lon { get; set; }

        public Manufactory()
        {
            id = Guid.NewGuid();
        }
    }
}
