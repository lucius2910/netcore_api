using Business.Plan.Contracts;

namespace Business.Plan.Interfaces
{
    public interface IProductPlanDayService
    {
        public  Task<IList<ProductPlanByDayResponse>> GetPaged(ProductPlanDaySearchRequest request);
    }
}
