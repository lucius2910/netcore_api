using Domain.Abstractions;

namespace Domain.Entities
{
    public class LogAction : BaseEntity
    {
        public string? path { get; set; }

        public string? method { get; set; }

        public string? body { get; set; }

        public string? message { get; set; }

        public string? ip { get; set; }

        public LogAction()
        {
            id = Guid.NewGuid();
        }
    }
}
