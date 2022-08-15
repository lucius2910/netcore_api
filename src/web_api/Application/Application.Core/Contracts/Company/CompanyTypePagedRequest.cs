using FluentValidation;
using Application.Common.Extensions;

namespace Application.Core.Contracts
{
    public class CompanyTypePagedRequest
    {
        public string type { get; set; }
        public int page { get; set; } = 1;
        public int size { get; set; } = 10;
        public string? sort { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string tel_no { get; set; }
        public string fax_no { get; set; }
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
