using Framework.Core.Entities;

namespace Seisankanri.Model.Entities
{
    public partial class Employee : BaseEntity
    {
        public string code { get; set; }

        public string full_name { get; set; }

        public string? email { get; set; }

        public string? phone { get; set; }

        public string? identity_number { get; set; }

        public DateTime? birthday { get; set; }

        public string position_cd { get; set; }

        public DateTime? start_date { get; set; }

        public DateTime? end_date { get; set; }

        public string status { get; set; }

        public Employee()
        {
            id = Guid.NewGuid();
        }
    }
}
