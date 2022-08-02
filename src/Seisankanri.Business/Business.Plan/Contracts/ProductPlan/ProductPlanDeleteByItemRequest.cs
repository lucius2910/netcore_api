using Business.Core.Interfaces;
using FluentValidation;
using Framework.Core.Extensions;
using Seisankanri.Business.Core.Extensions;

namespace Business.Plan.Contracts
{
    public class ProductPlanDeleteByItemRequest
    {
        public string? plan_month { get; set; }
        public string? item_cd { get; set; }
        public DateTime? plan_month_fomated
        {
            get { return plan_month.ToDate() != null ? plan_month.ToDate() : null; }
        }
    }
    public class ProductPlanDeleteByItemRequestValidator : AbstractValidator<ProductPlanDeleteByItemRequest>
    {
        public ProductPlanDeleteByItemRequestValidator(ILocalizeServices ls)
        {
            RuleFor(_ => _.plan_month).NotNullOrEmpty(ls).IsValidDate(ls);
            RuleFor(_ => _.item_cd).NotNullOrEmpty().MaximumLength(15);
        }
    }
}
