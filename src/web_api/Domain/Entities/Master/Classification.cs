using Domain.Abstractions;

namespace Domain.Entities
{
    public class Classification:BaseEntity
    {

        public string  code1 { get; set; }

        public string code2 { get; set; }

        public string name1 { get; set; }

        public string? name2 { get; set; }

        public string? value1 { get; set; }

        public string? value2 { get; set; }

        public string? remarks { get; set; }

        public int? order { get; set; }

        public virtual ICollection<Machine> machines { get; set; }
        public virtual ICollection<Machine> machines_type { get; set; }

        public virtual ICollection<User> user_departments { get; set; }

        public virtual ICollection<User> user_jobtitles { get; set; }

        public virtual ICollection<Item> item_types { get; set; }

        public Classification()
        {
            id = Guid.NewGuid();
        }
    }
}
