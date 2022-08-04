using FluentValidation;
using Seisankanri.Business.Core.Extensions;

namespace Business.Core.Contracts
{
    public class FunctionRequest
    {
        public string? module { get; set; }
        public string? code { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public string? url { get; set; }
        public string? path { get; set; }
        public string? method { get; set; }
        public int? order { get; set; }
        public bool? is_active { get; set; }
        public string? parent_cd { get; set; }
        public string? icon { get; set; }
    }

    public class FunctionRequestValidator : AbstractValidator<FunctionRequest>
    {
        public FunctionRequestValidator()
        {
            // TODO : check code duplicate
            RuleFor(_ => _.module).NotNullOrEmpty().MaximumLength(50);
            RuleFor(_ => _.code).NotNullOrEmpty().MaximumLength(50);
            RuleFor(_ => _.name).NotNullOrEmpty().MaximumLength(250);
            RuleFor(_ => _.description).MaximumLength(500);
        }
    }
}
