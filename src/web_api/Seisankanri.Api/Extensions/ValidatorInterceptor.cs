using Business.Core.Extensions;
using Business.Core.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Seisankanri.Api.Extensions
{
    public class ValidatorInterceptor : IValidatorInterceptor
    {
        private readonly ILocalizeServices _ls;

        public ValidatorInterceptor(ILocalizeServices ls)
        {
            _ls = ls;
        }
        public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
        {
            if (!result.IsValid)
            {
                throw new ValidationException(_ls.Get(Modules.CORE, "Message", MessageKey.E_005), result.Errors);
            }
            return result;
        }

        public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
        {
            return commonContext;
        }
    }
}
