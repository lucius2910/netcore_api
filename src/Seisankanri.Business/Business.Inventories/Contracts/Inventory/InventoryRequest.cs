using FluentValidation;
using Seisankanri.Business.Core.Extensions;

namespace Business.Inventories.Contracts
{
    public class InventoryRequest
    {
        public string? code { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public bool status { get; set; }
        public string? note { get; set; }
    }

    public class InventoryRequestValidator : AbstractValidator<InventoryRequest>
    {
        public InventoryRequestValidator()
        {
            // TODO : check code duplicate
            RuleFor(_ => _.code).NotNullOrEmpty().MaximumLength(50);
            RuleFor(_ => _.name).NotNullOrEmpty().MaximumLength(100);
            RuleFor(_ => _.address).NotNullOrEmpty().MaximumLength(100);
            RuleFor(_ => _.email).NotNullOrEmpty().MaximumLength(100);
            RuleFor(_ => _.phone).NotNullOrEmpty().MaximumLength(10);
        }
    }
}
