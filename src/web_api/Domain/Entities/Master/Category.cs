using Domain.Abstractions;


namespace Domain.Entities
{
   
    public partial class Category : BaseEntity
    {
        public string code { get; set;}

        public string name { get; set; }

        public string? description { get; set; }

        public Category()
        {
            id = Guid.NewGuid();
        }

    }
}
