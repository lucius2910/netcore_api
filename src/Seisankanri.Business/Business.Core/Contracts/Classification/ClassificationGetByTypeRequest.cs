using Business.Core.Interfaces;
using FluentValidation;
using Framework.Core.Abstractions;
using Seisankanri.Business.Core.Extensions;

namespace Business.Core.Contracts
{
    public class ClassificationGetByTypeRequest
    {
        public string type { get; set; }
    }
    public class ClassificationGetByTypeRequestValidator : AbstractValidator<ClassificationGetByTypeRequest>
    {
        public ClassificationGetByTypeRequestValidator(ILocalizeServices ls)
        {
            RuleFor(_ => _.type).NotNullOrEmpty(ls);
        }
    }
}
