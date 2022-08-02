using FluentValidation;
using Framework.Core.Collections;
using Seisankanri.Business.Core.Extensions;

namespace Business.Core.Contracts
{
    public class CompanyTypePagedRequest : PagedRequest
    {
        public string? type { get; set; }
        public string? name { get; set; }
        public string? code { get; set; }
        public string? tel_no { get; set; }
        public string? fax_no { get; set; }
    }
    public class CompanyTypePagedRequestValidator : AbstractValidator<CompanyTypePagedRequest>
    {
        public CompanyTypePagedRequestValidator()
        {
            // TODO : check code duplicate
            RuleFor(_ => _.type).NotNullOrEmpty();
        }
    }
}
