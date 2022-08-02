using Framework.Core.Entities;

namespace Seisankanri.Model.Entities
{
    public class Inventory : BaseEntity
    {

        public string? code { get; set; }

        public string? name { get; set; }

        public string? address { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public bool status { get; set; }

        public string? note { get; set; }

        public Inventory()
        {
            id = Guid.NewGuid();
        }
    }
}
