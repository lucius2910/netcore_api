using Business.Core.Interfaces;
using FluentValidation;
using Framework.Core.Abstractions;
using Framework.Core.Extensions;
using Seisankanri.Business.Core.Extensions;

namespace Business.Plan.Contracts
{
    public class SalePlanByBranchRequest
    {
        public string order_ym { get; set; }
        public string? item_name1 { get; set; }
        public string? item_cd { get; set; }
        public string? item_type { get; set; }
        public string? item_class_cd2 { get; set; }
        public DateTime? order_ym_fomated
        {
            get { return order_ym.ToDate() != null ? order_ym.ToDate() : null; }
        }

    }
    public class SalePlanSearchRequestValidator : AbstractValidator<SalePlanByBranchRequest>
    {
        public SalePlanSearchRequestValidator(IUnitOfWork unitOfWork, ILocalizeServices ls)
        {

            RuleFor(_ => _.order_ym).NotNullOrEmpty(ls).IsValidDate(ls);
        }
    }
}
