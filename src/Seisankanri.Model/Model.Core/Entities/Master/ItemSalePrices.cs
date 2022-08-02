using Framework.Core.Entities;

namespace Seisankanri.Model.Entities
{
    public partial class ItemSalePrices : BaseEntity
    {
        public string code { get; set; }

        public string item_cd { get; set; }

        public int price { get; set; }

        public string unit { get; set; }

        public DateTime effective_startdate { get; set; }

        public DateTime? effective_enddate { get; set; }

        public string? currency { get; set; }

        public Guid? customer { get; set; }

        public int min_qty { get; set; }

        public virtual Item item { get; set; }

        public ItemSalePrices()
        {
            id = Guid.NewGuid();
        }
    }
}
