using FluentValidation;
using Framework.Core.Extensions;
using Application.Common.Extensions;
using Domain.Entities;
using Application.Common.Abstractions;

namespace Application.Core.Contracts
{
    public class ReceiveOrderRequest
    {
        public string order_date { get; set; }

        public string company_cd { get; set; }

        public string branch_cd { get; set; }

        public string remarks { get; set; }

        public decimal? s_total_cost { get; set; }

        public string order_status { get; set; }

        public DateTime? order_date_fomated { 
            get { return order_date.ToDate() != null ? order_date.ToDate() : null; } 
        }

        public virtual List<ReceiveOrderDtRequest> details { get; set; }
    }
    public class ReceiveOrderRequestValidator : AbstractValidator<ReceiveOrderRequest>
    {
        public ReceiveOrderRequestValidator(IUnitOfWork unitOfWork, ILocalizeServices ls)
        {
            var repo_company = unitOfWork.GetRepository<Company>();
            var receiveOrderRepo = unitOfWork.GetRepository<ReceiveOrder>();

            RuleFor(_ => _.company_cd).NotNullOrEmpty(ls).MaxLength(ls, 15).MasterMustExist(repo_company, ls, "code");
            RuleFor(_ => _.branch_cd).NotNullOrEmpty(ls).MaxLength(ls, 160).MasterMustExist(repo_company, ls, "code");
            RuleFor(_ => _.order_date).NotNullOrEmpty(ls).IsValidDate(ls);
            RuleFor(_ => _.remarks).MaxLength(ls, 160);
        }
    }
}
