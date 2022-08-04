using Business.Core.Interfaces;
using FluentValidation;
using Framework.Core.Abstractions;
using Framework.Core.Extensions;
using Seisankanri.Business.Core.Extensions;

namespace Business.Plan.Contracts
{
    public class ProductPlanDayRequest
    {
        public string? machine_cd { get; set; }
        public string plan_day { get; set; }
        public int? to_be_sold { get; set; }
        public int? production_schedule { get; set; }
        public int? production_results { get; set; }
        public DateTime? plan_day_fomated
        {
            get { return plan_day.ToDate() != null ? plan_day.ToDate() : null; }
        }
    }
    public class ProductPlanDayRequestValidator : AbstractValidator<ProductPlanDayRequest>
    {
        public ProductPlanDayRequestValidator(IUnitOfWork unitOfWork, ILocalizeServices ls)
        {
            var machineRepo = unitOfWork.GetRepository<Seisankanri.Model.Entities.Machine>();
            RuleFor(_ => _.plan_day).NotNullOrEmpty(ls).IsValidDate(ls);
            RuleFor(_ => _.machine_cd).NotNullOrEmpty(ls).MaxLength(ls,15).MasterMustExist(machineRepo, ls, "code");
        }
    }
}
