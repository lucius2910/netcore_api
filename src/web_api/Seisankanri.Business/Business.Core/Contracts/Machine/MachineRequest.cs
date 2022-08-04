using Business.Core.Interfaces;
using FluentValidation;
using Framework.Core.Extensions;
using Seisankanri.Business.Core.Extensions;

namespace Business.Core.Contracts
{
    public class MachineRequest
    {
        public string code { get; set; }
        public string name { get; set; }
        public string department_cd { get; set; }
        public string supplier_cd { get; set; }
        public Guid admin_id { get; set; }
        public string? mold_cd { get; set; }
        public string? type { get; set; }
        public int? size1 { get; set; }
        public int? size2 { get; set; }
        public int? size3 { get; set; }
        public string? color { get; set; }
        public string effective_date { get; set; }
        public string remarks { get; set; }

        public DateTime? effective_date_format
        {
            get
            {
                return effective_date.ToDateTime();
            }
        }
    }
    public class MachineRequestValidator : AbstractValidator<MachineRequest>
    {
        public MachineRequestValidator(ILocalizeServices ls)
        {
            RuleFor(_ => _.code).NotNullOrEmpty().MaximumLength(15);
            RuleFor(_ => _.name).NotNullOrEmpty().MaximumLength(15);
            RuleFor(_ => _.mold_cd).MaximumLength(15);
            RuleFor(_ => _.type).MaximumLength(15);
            RuleFor(_ => _.remarks).NotNullOrEmpty().MaximumLength(240);
            RuleFor(_ => _.effective_date).IsValidDateTime(ls);
        }
    }
}