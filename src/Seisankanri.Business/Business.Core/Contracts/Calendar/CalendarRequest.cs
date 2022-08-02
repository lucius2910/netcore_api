using Business.Core.Interfaces;
using FluentValidation;
using Framework.Core.Abstractions;
using Framework.Core.Extensions;
using Seisankanri.Business.Core.Extensions;
using Seisankanri.Model.Core;
using Seisankanri.Model.Entities;

namespace Business.Core.Contracts
{
    public class CalendarRequest
    {
        public string? calendar_date { get; set; }
        public string company_cd { get; set; }
        public CalendarStatus open_status { get; set; }
        public DateTime? calendar_date_formated
        { get {
                return this.calendar_date.ToDate();
            } }
    }
    public class CalendarRequestValidator : AbstractValidator<CalendarRequest>
    {
        public CalendarRequestValidator(IUnitOfWork _unitOfWork, ILocalizeServices _ls)
        {
            var repo = _unitOfWork.GetRepository<Company>();
            RuleFor(_ => _.company_cd).NotNullOrEmpty().MasterMustExist(repo, _ls, "code");
            RuleFor(_ => _.calendar_date).NotNullOrEmpty().IsValidDate(_ls);
            RuleFor(_ => _.open_status).NotNullOrEmpty().IsInEnum();
        }
    }
}
