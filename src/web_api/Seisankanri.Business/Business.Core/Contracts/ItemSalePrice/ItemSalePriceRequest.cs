using FluentValidation;
using Seisankanri.Business.Core.Extensions;

namespace Business.Core.Contracts
{
    public class ItemSalePriceRequest
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
    }

    public class ItemSalePriceRequestValidator : AbstractValidator<ItemSalePriceRequest>
    {
        public ItemSalePriceRequestValidator()
        {
            // TODO : check code duplicate
            RuleFor(_ => _.code).NotNullOrEmpty().MaximumLength(15);
            RuleFor(_ => _.item_cd).NotNullOrEmpty().MaximumLength(15);
            RuleFor(_ => _.unit).NotNullOrEmpty().MaximumLength(15);   
            RuleFor(_ => _.currency).MaximumLength(3);
        }
    }
}
