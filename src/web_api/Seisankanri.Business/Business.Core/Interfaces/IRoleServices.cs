using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Business.Core.Contracts;

namespace Business.Core.Interfaces
{
    public interface IRoleServices
    {
        Task<IPagedList<RoleResponse>> GetPaged(RolePagedRequest request);
        Task<int> Create(RoleRequest request);
        Task<RoleResponse> GetById(Guid id);
        Task<int> Update(Guid id, RoleRequest request);
        Task<int> Delete(Guid id);

    }
}
