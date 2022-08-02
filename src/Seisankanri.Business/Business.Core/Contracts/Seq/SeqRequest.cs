using Business.Core.Interfaces;
using FluentValidation;
using Seisankanri.Business.Core.Extensions;

namespace Business.Core.Contracts
{
    public class SeqRequest
    {
        public string code { get; set; }
    }
    public class SeqRequestValidator : AbstractValidator<SeqRequest>
    {
        public SeqRequestValidator(ILocalizeServices ls)
        {
            RuleFor(_ => _.code).NotNullOrEmpty(ls);
        }
    }
}
