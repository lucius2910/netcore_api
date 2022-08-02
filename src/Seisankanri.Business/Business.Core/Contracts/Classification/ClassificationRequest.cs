using FluentValidation;
using Seisankanri.Business.Core.Extensions;

namespace Business.Core.Contracts
{
    public class ClassificationRequest
    {
        public Guid id { get; set; }
        public string code1 { get; set; }
        public string code2 { get; set; }
        public string name1 { get; set; }
        public string? name2 { get; set; }
        public string? value1 { get; set; }
        public string? value2 { get; set; }
        public string? remarks { get; set; }
        public bool is_delete { get; set; }
    }

    public class ClassificationRequestValidator : AbstractValidator<ClassificationRequest>
    {
        public ClassificationRequestValidator()
        {
            // TODO : check code duplicate
            RuleFor(_ => _.code1).NotNullOrEmpty().MaximumLength(15);
            RuleFor(_ => _.code2).NotNullOrEmpty().MaximumLength(15);
            RuleFor(_ => _.name1).NotNullOrEmpty().MaximumLength(160);
            RuleFor(_ => _.name2).MaximumLength(160);
            RuleFor(_ => _.value1).MaximumLength(160);
            RuleFor(_ => _.value1).MaximumLength(160);
            RuleFor(_ => _.remarks).MaximumLength(240);
        }
    }
}

