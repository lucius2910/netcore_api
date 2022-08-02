using Business.Core.Interfaces;
using FluentValidation;
using Framework.Core.Abstractions;
using Framework.Core.Extensions;
using Seisankanri.Business.Core.Extensions;
using Seisankanri.Model.Entities;

namespace Business.Plan.Contracts
{
    public class SalePlanByMonthRequest
    {
        public string order_ym { get; set; }
        public string item_name1 { get; set; }
        public string item_edi_cd { get; set; }
        public string item_type { get; set; }
        public string? item_class_cd2 { get; set; }
        public string company_cd { get; set; }
        public DateTime? order_ym_fomated
        {
            get { return order_ym.ToDate() != null ? order_ym.ToDate() : null; }
        }
    }

    public class SalePlanGetByMonthRequestValidator : AbstractValidator<SalePlanByMonthRequest>
    {
        public SalePlanGetByMonthRequestValidator(IUnitOfWork unitOfWork, ILocalizeServices ls)
        {
            var repo_saleplan = unitOfWork.GetRepository<SalePlan>();

            RuleFor(_ => _.order_ym).NotNullOrEmpty(ls).IsValidDate(ls);
        }
    }
}
