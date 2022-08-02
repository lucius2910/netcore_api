using FluentValidation;
using Seisankanri.Business.Core.Extensions;

namespace Business.Inventories.Contracts
{
    public class WarehouseRequest
    {
        public string type { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string control_company { get; set; }
        public string control_cd { get; set; }
        public string postcode { get; set; }
        public string? address { get; set; }
        public string? phone { get; set; }
        public string capacity { get; set; }
        public int rent_price { get; set; }
        public string? item_info { get; set; }
        public string? person_cd1 { get; set; }
        public string? person_cd2 { get; set; }
        public string? remarks { get; set; }
    }

    public class WarehouseRequestValidator : AbstractValidator<WarehouseRequest>
    {
        public WarehouseRequestValidator()
        {
            // TODO : check code duplicate
            RuleFor(_ => _.type).NotNullOrEmpty().MaximumLength(15);
            RuleFor(_ => _.code).NotNullOrEmpty().MaximumLength(15);
            RuleFor(_ => _.name).NotNullOrEmpty().MaximumLength(15);
            RuleFor(_ => _.control_company).NotNullOrEmpty().MaximumLength(15);
            RuleFor(_ => _.control_cd).NotNullOrEmpty().MaximumLength(15);
            RuleFor(_ => _.postcode).NotNullOrEmpty().MaximumLength(15);
            RuleFor(_ => _.address).MaximumLength(160);
            RuleFor(_ => _.phone).MaximumLength(15);
            RuleFor(_ => _.capacity).NotNullOrEmpty().MaximumLength(15);
            RuleFor(_ => _.item_info).MaximumLength(160);
            RuleFor(_ => _.person_cd1).MaximumLength(15);
            RuleFor(_ => _.person_cd2).MaximumLength(15);
            RuleFor(_ => _.remarks).MaximumLength(240);

        }
    }
}
