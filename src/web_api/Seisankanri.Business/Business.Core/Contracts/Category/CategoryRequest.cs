using FluentValidation;
using Seisankanri.Business.Core.Extensions;

namespace Business.Core.Contracts
{
    public class CategoryRequest
    {
        public string code { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
    }
    public class CategoryRequestValidator : AbstractValidator<CategoryRequest>
    {
        public CategoryRequestValidator()
        {
            RuleFor(_ => _.code).NotNullOrEmpty().MaximumLength(50);
            RuleFor(_ => _.name).NotNullOrEmpty().MaximumLength(100);
            RuleFor(_ => _.description).MaximumLength(200);
        }
    }
}
