using Framework.Core.Entities;

namespace Seisankanri.Model.Entities
{
    public partial class Warehouses : BaseEntity
    {
        public string type { get; set; }

        public string code { get; set; }

        public string name { get; set; }

        public string control_company { get; set; }

        public string control_cd { get; set; }

        public string postcode { get; set; }

        public string? address { get; set; }

        public string? phone { get; set; }

        public string capacity { get; set; }

        public int rent_price { get; set; }

        public string? item_info { get; set; }

        public string? person_cd1 { get; set; }

        public string? person_cd2 { get; set; }

        public string? remarks { get; set; }

        public Warehouses()
        {
            id = Guid.NewGuid();
        }
    }
}
