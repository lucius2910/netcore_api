using Business.Plan.Contracts;

namespace Business.Plan.Interfaces
{
    public interface ISalePlanServices
    {
        Task<int> CreateUpdate(List<SalePlanRequest> request);
        Task<IList<SalePlanByMonthResponse>> GetByMonth(SalePlanByMonthRequest request);
        Task<IList<SalePlanByBrandResponse>> GetByBranch(SalePlanByBranchRequest request);
    }
}
