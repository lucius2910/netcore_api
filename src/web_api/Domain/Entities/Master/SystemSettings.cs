using Domain.Abstractions;

namespace Domain.Entities
{
    public partial class SystemSettings : BaseEntity
    {
        public string code { get; set; }

        public string? name { get; set; }

        public string? value { get; set; }

    }
}
