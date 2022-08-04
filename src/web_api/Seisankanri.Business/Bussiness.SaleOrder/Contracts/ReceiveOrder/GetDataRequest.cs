using Business.Core.Interfaces;
using FluentValidation;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Seisankanri.Business.Core.Extensions;
using Seisankanri.Model.Entities;


namespace Business.SaleOrder.Contracts
{
    public class GetDataRequest : PagedRequest
    {
        public Guid order_id { get; set; }
    }
    public class GetDataRequestValidator : AbstractValidator<GetDataRequest>
    {
        public GetDataRequestValidator(IUnitOfWork _unitOfWork, ILocalizeServices _ls)
        {
            var repo = _unitOfWork.GetRepository<ReceiveOrder>();
            // TODO : check code duplicate
            RuleFor(_ => _.order_id).MasterMustExist(repo, _ls, "id");
        }
    }
}

