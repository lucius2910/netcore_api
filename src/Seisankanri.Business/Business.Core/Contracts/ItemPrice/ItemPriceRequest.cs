using FluentValidation;
using Seisankanri.Business.Core.Extensions;

namespace Business.Core.Contracts
{
    public class ItemPriceRequest
    {
        public string item_cd { get; set; }
        public int? buy_price { get; set; }
        public int? sale_price { get; set; }
        public string? unit { get; set; }
        public int? basic_qty { get; set; }
        public int? meterial_price { get; set; }
        public int? process_price { get; set; }
        public int? auxiliary_price { get; set; }
        public DateTime? effective_startdate { get; set; }
        public DateTime? effective_enddate { get; set; }
        public string? currency { get; set; }
        public Guid? customer { get; set; }
        public int? min_qty { get; set; }
        public string? item { get; set; }
    }
    public class ItemPriceRequestValidator : AbstractValidator<ItemPriceRequest>
    {
        public ItemPriceRequestValidator()
        {
            // TODO : check code duplicate
            RuleFor(_ => _.item_cd).NotNullOrEmpty().MaximumLength(50);
            RuleFor(_ => _.unit).NotNullOrEmpty().MaximumLength(100);
            RuleFor(_ => _.currency).NotNullOrEmpty().MaximumLength(250);
        }
    }
}
