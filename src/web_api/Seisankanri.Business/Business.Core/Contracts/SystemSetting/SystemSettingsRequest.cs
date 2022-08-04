using FluentValidation;
using Seisankanri.Business.Core.Extensions;

namespace Business.Core.Contracts
{
    public class SystemSettingsRequest
    {
        public string code { get; set; }
        public string? name { get; set; }
        public string? value { get; set; }
    }
    public class SystemSettingsRequestValidator : AbstractValidator<SystemSettingsRequest>
    {
        public SystemSettingsRequestValidator()
        {
            RuleFor(_ => _.code).NotNullOrEmpty().MaximumLength(15);
            RuleFor(_ => _.name).MaximumLength(160);
            RuleFor(_ => _.value).MaximumLength(160);
        }
    }
}
