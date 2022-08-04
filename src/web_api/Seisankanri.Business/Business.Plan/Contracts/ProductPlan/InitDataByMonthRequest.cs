using Business.Core.Interfaces;
using FluentValidation;
using Framework.Core.Extensions;
using Seisankanri.Business.Core.Extensions;

namespace Business.Plan.Contracts
{
    public class InitDataByMonthRequest
    {
        public string order_ym { get; set; }
        public DateTime? order_ym_fomated
        {
            get { return order_ym.ToDate() != null ? order_ym.ToDate() : null; }
        }
    }
    public class InitDataByMonthRequestValidator : AbstractValidator<InitDataByMonthRequest>
    {
        public InitDataByMonthRequestValidator(ILocalizeServices ls)
        {
            RuleFor(_ => _.order_ym).NotNullOrEmpty(ls).IsValidDate(ls);
        }
    }

}
