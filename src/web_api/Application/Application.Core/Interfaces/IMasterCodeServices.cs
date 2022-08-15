using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Application.Core.Contracts;

namespace Application.Core.Interfaces
{
    public interface IMasterCodeServices
    {
        Task<IPagedList<MasterCodeRespose>> GetPaged(PagedRequest request);
        Task<IPagedList<MasterCodeRespose>> GetByTypePaged(MasterCodePagedRequest request);
        Task<MasterCodeRespose> GetById(Guid id);
        Task<int> Create(MasterCodeRequest request);
        Task<int> Update(Guid id, MasterCodeRequest request);
        Task<int> Delete(Guid id);
        Task<int> Delete(Guid[] ids);
    }
}
