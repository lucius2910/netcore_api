using Business.Core.Interfaces;
using FluentValidation;
using Framework.Core.Extensions;
using Seisankanri.Business.Core.Extensions;

namespace Business.Plan.Contracts
{
    public class ProductPlanDaySearchRequest
    {
        public string? sort { get; set; }
        public string plan_month { get; set; }
        public string item_cd { get; set; }
        public string item_name { get; set; }
        public string? item_class_cd2 { get; set; }
        public DateTime? plan_month_formated
        {
            get
            {
                return this.plan_month.ToDateTime();
            }
        }
    }
    public class ProductPlanDaySearchRequestValidator : AbstractValidator<ProductPlanDaySearchRequest>
    {
        public ProductPlanDaySearchRequestValidator(ILocalizeServices ls)
        {
            RuleFor(_ => _.plan_month).NotNullOrEmpty().IsValidDateTime(ls);
        }
    }
}
