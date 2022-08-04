using Business.Core.Interfaces;
using FluentValidation;
using Framework.Core.Extensions;
using Seisankanri.Business.Core.Extensions;

namespace Business.Plan.Contracts
{
    public class ProductPlanMonthRequest
    {
        public string? sort { get; set; }
        public string item_type { get; set; }
        public string plan_month { get; set; }
        public string item_cd { get; set; }
        public string? item_name { get; set; }
        public DateTime? plan_month_fomated
        {
            get { return plan_month.ToDate() != null ? plan_month.ToDate() : null; }
        }
    }

    public class ProductPlanMonthRequestValidator : AbstractValidator<ProductPlanMonthRequest>
    {
        public ProductPlanMonthRequestValidator(ILocalizeServices ls)
        {
            RuleFor(_ => _.plan_month).NotNullOrEmpty(ls).IsValidDate(ls);
            RuleFor(_ => _.item_cd).MaximumLength(15);
            RuleFor(_ => _.item_name).MaximumLength(160);
        }
    }
}
