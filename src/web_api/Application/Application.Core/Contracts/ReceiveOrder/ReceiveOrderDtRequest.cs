using FluentValidation;
using Framework.Core.Extensions;
using Application.Common.Extensions;
using Application.Common.Abstractions;
using Domain.Entities;

namespace Application.Core.Contracts
{
    public class ReceiveOrderDtRequest
    {
        public string item_cd { get; set; }
        public string item_name { get; set; }
        public string delivery_cd { get; set; }
        public decimal r_order_input { get; set; }
        public decimal r_order_pieces_qty { get; set; }
        public decimal r_order_qty { get; set; }
        public decimal r_order_unit_price { get; set; }
        public decimal r_order_cost { get; set; }
        public string r_order_dl { get; set; }
        public string remarks { get; set; }
        public DateTime? r_order_dl_fomated
        {
            get { return r_order_dl.ToDate() != null ? r_order_dl.ToDate() : null; }
        }
    }
    public class ReceiveOrderDtRequestValidator : AbstractValidator<ReceiveOrderDtRequest>
    {
        public ReceiveOrderDtRequestValidator(IUnitOfWork _unitOfWork, ILocalizeServices _ls)
        {
            var repo_item = _unitOfWork.GetRepository<Item>();
            var repo_company = _unitOfWork.GetRepository<Company>();

            RuleFor(_ => _.item_cd).NotNullOrEmpty(_ls).MaxLength(_ls, 15).MasterMustExist(repo_item, _ls, "code");
            RuleFor(_ => _.delivery_cd).NotNullOrEmpty(_ls).MaxLength(_ls, 15).MasterMustExist(repo_company, _ls, "code");
            RuleFor(_ => _.item_name).MaxLength(_ls, 160);
            RuleFor(_ => _.remarks).MaxLength(_ls, 160);
            RuleFor(_ => _.r_order_dl).IsValidDate(_ls);
        }
    }
}
