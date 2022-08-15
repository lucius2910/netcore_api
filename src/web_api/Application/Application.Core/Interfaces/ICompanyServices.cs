using Application.Core.Contracts;
using Framework.Core.Abstractions;
using Framework.Core.Collections;

namespace Application.Core.Interfaces
{
    public interface ICompanyServices
    {
        Task<IPagedList<CompanyResponse>> GetPaged(PagedRequest request);
        Task<CompanyResponse> GetById(Guid id);
        Task<int> Create(CompanyRequest request);
        Task<int> Update(Guid id, CompanyRequest request);
        Task<int> Delete(Guid id);
        Task<IPagedList<CompanyResponse>> GetListByType(CompanyTypePagedRequest request);
    }
}
