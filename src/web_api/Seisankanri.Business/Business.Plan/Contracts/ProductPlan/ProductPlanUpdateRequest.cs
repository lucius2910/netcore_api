using FluentValidation;
using Seisankanri.Business.Core.Extensions;

namespace Business.Plan.Contracts
{
    public class ProductPlanUpdateRequest
    {
        public string? item_cd { get; set; }
        public List<ProductPlanDayRequest>? data { get; set; }
    }
    public class ProductPlanUpdateRequestValidator : AbstractValidator<ProductPlanUpdateRequest>
    {
        public ProductPlanUpdateRequestValidator()
        {
            RuleFor(_ => _.item_cd).NotNullOrEmpty().MaximumLength(15);
        }
    }
}
