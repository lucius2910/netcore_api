using Business.Core.Interfaces;
using FluentValidation;
using Framework.Core.Abstractions;
using Seisankanri.Business.Core.Extensions;
using Seisankanri.Model.Entities;

namespace Business.Plan.Contracts
{
    public class SalePlanRequest
    {
        public Guid id { get; set; }
        public string company_cd { get; set; }
        public string item_type { get; set; }
        public string item_cd { get; set; }
        public string item_edi_cd { get; set; }
        public string item_name1 { get; set; }
        public string item_name2 { get; set; }
        public string order_unit { get; set; }
        public int standad_unit_qty { get; set; }
        public List<SalePlanOrderYmQty>? data { get; set; }
    }

    public class SalePlanRequestValidator : AbstractValidator<SalePlanRequest>
    {
        public SalePlanRequestValidator(IUnitOfWork unitOfWork, ILocalizeServices ls)
        {
            var companyRepo = unitOfWork.GetRepository<Company>();
            var classificationRepo = unitOfWork.GetRepository<Classification>();
            var itemRepo = unitOfWork.GetRepository<Item>();
            var itemEdiRepo = unitOfWork.GetRepository<ItemEdi>();

            RuleFor(_ => _.company_cd).MaxLength(ls,15).NotNullOrEmpty(ls).MasterMustExist(companyRepo, ls, "code");
            RuleFor(_ => _.item_type).MaxLength(ls, 15).NotNullOrEmpty(ls).MasterMustExist(classificationRepo, ls, "code2");
            RuleFor(_ => _.item_cd).MaxLength(ls, 15).NotNullOrEmpty(ls).MasterMustExist(itemRepo, ls, "code");
            RuleFor(_ => _.item_edi_cd).MaxLength(ls, 15).MasterMustExist(itemEdiRepo, ls, "edi_cd");
            RuleFor(_ => _.item_name1).MaxLength(ls, 160).NotNullOrEmpty(ls);
            RuleFor(_ => _.item_name2).MaxLength(ls, 160);
            RuleFor(_ => _.order_unit).MaxLength(ls, 15).NotNullOrEmpty(ls);
            RuleFor(_ => _.standad_unit_qty).NotNullOrEmpty(ls);
        }
    }

}
