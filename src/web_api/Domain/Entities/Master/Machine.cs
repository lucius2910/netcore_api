using Domain.Abstractions;

namespace Domain.Entities
{
    public partial class Machine : BaseEntity
    {
        public string code { get; set; }

        public string name { get; set; }

        public string department_cd { get; set; }

        public string supplier_cd { get; set; }

        public string admin_cd { get; set; }

        public string? mold_cd { get; set; }

        public string? type { get; set; }

        public int? size1 { get; set; }

        public int? size2 { get; set; }

        public int? size3 { get; set; }

        public string? color { get; set; }

        public DateTime effective_date { get; set; }

        public string remarks { get; set; }

        public virtual User? admin { get; set; }

        public virtual Classification? department { get; set; }
        public virtual Classification? classification_type { get; set; }

        public virtual Company? supplier { get; set; }

        public virtual ICollection<Item>? items { get; set; }

        public virtual ICollection<ProductPlanDay>? product_plan_days { get; set; }

        public Machine()
        {
            id = Guid.NewGuid();
        }
    }
}
