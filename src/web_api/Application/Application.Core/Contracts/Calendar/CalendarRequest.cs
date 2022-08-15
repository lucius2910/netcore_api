using Application.Common.Abstractions;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using Application.Common.Extensions;

namespace Application.Core.Contracts
{
    public class CalendarRequest
    {
        public DateTime? calendar_date { get; set; }
        public string company_cd { get; set; }
        public CalendarStatus open_status { get; set; }
        public virtual Company? company { get; set; }
    }
    public class CalendarRequestValidator : AbstractValidator<CalendarRequest>
    {
        public CalendarRequestValidator(IUnitOfWork _unitOfWork, ILocalizeServices _ls)
        {
            var repo = _unitOfWork.GetRepository<Company>();
            RuleFor(_ => _.company_cd).NotNullOrEmpty().MasterMustExist(repo, _ls, "code");
            RuleFor(_ => _.open_status).NotNullOrEmpty().IsInEnum();
        }
    }
}
