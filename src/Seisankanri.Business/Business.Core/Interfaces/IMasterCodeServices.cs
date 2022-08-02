using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Business.Core.Contracts;

namespace Business.Core.Interfaces
{
    public interface IMasterCodeServices
    {
        Task<IPagedList<MasterCodeRespose>> GetPaged(MasterCodePagedRequest request);
        Task<IPagedList<MasterCodeRespose>> GetByTypePaged(MasterCodePagedByTypeRequest request);
        Task<MasterCodeRespose> GetById(Guid id);
        Task<int> Create(MasterCodeRequest request);
        Task<int> Update(Guid id, MasterCodeRequest request);
        Task<int> Delete(Guid id);
        Task<int> Delete(Guid[] ids);
    }
}
