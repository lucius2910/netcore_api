using FluentValidation;
using Seisankanri.Business.Core.Extensions;

namespace Business.Core.Contracts
{
    public class ItemPriceMasterRequest
    {
        public string? item_type { get; set; }
        public string code { get; set; }
        public string? name1 { get; set; }
        public string? name2 { get; set; }
        public ItemPriceRequest? item_price { get; set; }
        public List<ItemBuyPriceRequest>? buy_prices { get; set; }
        public List<ItemSalePriceRequest>? sale_prices { get; set; }
    }

    public class ItemPriceMasterRequestValidator : AbstractValidator<ItemPriceMasterRequest>
    {
        public ItemPriceMasterRequestValidator()
        {
            // TODO : check code duplicate
            RuleFor(_ => _.item_type).NotNullOrEmpty().MaximumLength(10);
            RuleFor(_ => _.code).NotNullOrEmpty().MaximumLength(15);
            RuleFor(_ => _.name1).NotNullOrEmpty().MaximumLength(160);
            RuleFor(_ => _.name2).NotNullOrEmpty().MaximumLength(160);
        }
    }
}
