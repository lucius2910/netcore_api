using Business.Core.Interfaces;
using FluentValidation;
using Framework.Core.Abstractions;
using Seisankanri.Business.Core.Extensions;
using Seisankanri.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Contracts
{
    public class ItemEdiUpdateRequest
    {
        public string company_cd { get; set; }

        public string item_type { get; set; }

        public string item_cd { get; set; }

        public string item_name1 { get; set; }

        public string item_name2 { get; set; }

        public DateTime export_date { get; set; }

        public bool del_flg { get; set; }
    }

    public class ItemEdiUpdateRequestValidator : AbstractValidator<ItemEdiUpdateRequest>
    {
        public ItemEdiUpdateRequestValidator(IUnitOfWork _unitOfWork, ILocalizeServices _ls)
        {
            var repoCompany = _unitOfWork.GetRepository<Company>();
            var repoItem = _unitOfWork.GetRepository<Item>();

            RuleFor(_ => _.company_cd).NotNullOrEmpty().MaximumLength(15).MasterMustExist(repoCompany, _ls, "code");
            RuleFor(_ => _.item_cd).NotNullOrEmpty().MaximumLength(15).MasterMustExist(repoItem, _ls, "code"); ;
            RuleFor(_ => _.item_type).NotNullOrEmpty().MaximumLength(15);
            RuleFor(_ => _.item_name1).NotNullOrEmpty().MaximumLength(160);
            RuleFor(_ => _.item_name2).NotNullOrEmpty().MaximumLength(160);
            RuleFor(_ => _.del_flg).NotNullOrEmpty();
        }
    }
}
