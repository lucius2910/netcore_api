using Business.Core.Contracts;
using Framework.Core.Abstractions;

namespace Business.Core.Interfaces
{
    public interface ICompanyServices
    {
        Task<IPagedList<CompanyResponse>> GetPaged(CompanyPagedRequest request);
        Task<CompanyResponse> GetById(Guid id);
        Task<int> Create(CompanyRequest request);
        Task<int> Update(Guid id, CompanyRequest request);
        Task<int> Delete(Guid id);
        Task<IPagedList<CompanyResponse>> GetListByType(CompanyTypePagedRequest request);
    }
}
