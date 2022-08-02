using FluentValidation;
using Seisankanri.Business.Core.Extensions;

namespace Business.Core.Contracts
{
    public class RoleRequest
    {
        public string code { get; set; }
        public string name { get; set; }
        public string is_actived { get; set; }
        public string? description { get; set; }
        public List<string> permissions { get; set; }
    }

    public class RoleRequestValidator : AbstractValidator<RoleRequest>
    {
        public RoleRequestValidator()
        {
            // TODO : check code duplicate
            RuleFor(_ => _.code).NotNullOrEmpty().MaximumLength(15);
            RuleFor(_ => _.name).NotNullOrEmpty().MaximumLength(160);
            RuleFor(_ => _.description).MaximumLength(240);
            RuleFor(_ => _.is_actived).NotNullOrEmpty().MaximumLength(1);
        }
    }
}
