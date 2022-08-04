using Business.Plan.Contracts;

namespace Business.Plan.Interfaces
{
    public interface IProductPlanService
    {
        Task<IList<ProductPlanByDayResponse>> GetByDay(ProductPlanDaySearchRequest request);
        List<ProductPlanByDayResponse> GetDataItemPlan(ProductPlanDaySearchRequest request);
        Task<IList<ProductPlanByMonthResponse>> GetByMonth(ProductPlanMonthRequest request);
        Task<int> DeleteByItem(ProductPlanDeleteByItemRequest request);
        Task<int> UpdateByMonth(List<ProductPlanUpdateRequest> requests);
        bool CheckDataExit(DateTime plan_month);
        Task<int> InitDataByMonth(InitDataByMonthRequest request);
    }
}
