using Domain.Abstractions;

namespace Domain.Entities
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
